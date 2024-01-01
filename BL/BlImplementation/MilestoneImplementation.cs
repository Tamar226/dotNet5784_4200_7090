using BlApi;
using BO;
using DalApi;
using DO;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;//s_dal Initiated to work with the supporting departmen In the Factory design template that initializes the appropriate class according to the configuration file:
    /// <summary>
    /// Creating all the milestones by reading from the list of dependencies created in XML
    /// and assigning a milestone between every 2 tasks according to the required rules
    /// </summary>
    private List<DO.Dependence> createAllMilestones(List<DO.Dependence?> dependencies)
    {
        List<DO.Dependence> listOfNewDependencies = new List<DO.Dependence>();
        var listOfGroupDependencies = (
           from dep in dependencies
           where dep.DependentTask is not null && dep.DependsOnTask is not null
           group dep by dep.DependentTask into newList
           let depList = (from dep in newList
                          select dep.DependsOnTask)
           select new { _Key = newList.Key, Value = depList.Order() }).ToList();
        var listWithoutDuplicates = listOfGroupDependencies;
        for (int i = 0; i < listOfGroupDependencies.Count(); i++)//מחיקת כפלויות
        {
            var nulla = from d in listOfGroupDependencies
                        where d._Key != listOfGroupDependencies[i]._Key && d.Value.SequenceEqual(listOfGroupDependencies[i].Value)
                        select d._Key;
            if (nulla.Count() >= 1)
            {
                listWithoutDuplicates.Remove(listWithoutDuplicates[i]);
            }
        }
        int runningNameForMilestone = 1;
        var secondReadDep = (
          from dep in dependencies
          where dep.DependentTask is not null && dep.DependsOnTask is not null
          group dep by dep.DependentTask into newList
          let depList = (from dep in newList
                         select dep.DependsOnTask)
          select new { _Key = newList.Key, Value = depList.Order() }).ToList();
        //Adding a milestone with all the required parameters to the list, wherever it is intended
        foreach (var tasks in listWithoutDuplicates)
        {
            List<int?> deptList = new List<int?>();
            int milestoneId = _dal.Task.Create(new DO.Task(0,  "milestone", "M" + runningNameForMilestone, DateTime.Now, TimeSpan.Zero, true));
            foreach (var task in tasks.Value)
            {
                deptList.Add(task);
                int depToAdd = _dal.Dependence.Create(new DO.Dependence(0, milestoneId, task));
                listOfNewDependencies.Add(_dal.Dependence.Read(depToAdd)!);
            }

            foreach (var task in secondReadDep)
            {
                if (task.Value.SequenceEqual(deptList))
                    listOfNewDependencies.Add(new DO.Dependence(0, task._Key, milestoneId));
            }
            runningNameForMilestone++;
        }
        //Saving with a special name only for the first and last milestone
        int firstMilestoneId = _dal.Task.Create(new DO.Task(0, "Start", "Start", DateTime.Now, TimeSpan.Zero, true));
        int lastMilestoneId = _dal.Task.Create(new DO.Task(0, "End", "End", DateTime.Now, TimeSpan.Zero, true));

        foreach (var dep in dependencies)
        {
            if (dep.DependsOnTask == null)//Those that depend on the first milestone
                listOfNewDependencies.Add(new DO.Dependence(dep.IdNumberDependence, dep.DependentTask, firstMilestoneId));
            else if (dep.DependentTask == null)//Those on whom the last stepping stone depends
                listOfNewDependencies.Add(new DO.Dependence(dep.IdNumberDependence, lastMilestoneId, dep.DependsOnTask));
        }
        return listOfNewDependencies;

    }
    /// <summary>
    /// Creating a Loz Project - calculates the start time and end time for each task, 
    /// and saves them in the correct order after calling the existing dependency list
    /// </summary>
    public void CreateProjectSchedule(DateTime? startDate,DateTime? endDate)
    {
        List<DO.Dependence?> dependencies = _dal.Dependence.ReadAll().ToList();
        List<DO.Dependence> listOfNewDependencies = createAllMilestones(dependencies);
        List<DO.Task?> tasks = _dal.Task.ReadAll().ToList();

        _dal.Dependence.Reset();

        foreach (var dep in listOfNewDependencies)
        {
            _dal.Dependence.Create(dep);
        }
        _dal.StartDateToProject = startDate;
        _dal.EndDateToProject =endDate;
        DO.Task? firstMilestone = tasks.Where(task => task!.Alias == "Start").Select(task => task).First();
        DO.Task? lastMilestone = tasks.Where(task => task!.Alias == "End").Select(task => task).First();
        //Updating the most important and last tasks for the date set for them
        _dal.Task.Update(new DO.Task(firstMilestone!.IdNumberTask, firstMilestone.Description, firstMilestone.Alias, firstMilestone.CreatedAtDate, firstMilestone.RequiredEffortTime, firstMilestone.Milestone, firstMilestone.Product, firstMilestone.Notes, firstMilestone.Level, firstMilestone.idEngineer, firstMilestone.StartDate, _dal.StartDateToProject, firstMilestone.LastEndDate, firstMilestone.ActualEndDate));
        _dal.Task.Update(new DO.Task(lastMilestone!.IdNumberTask, lastMilestone.Description, lastMilestone.Alias, lastMilestone.CreatedAtDate, lastMilestone.RequiredEffortTime, lastMilestone.Milestone, lastMilestone.Product, lastMilestone.Notes, lastMilestone.Level, lastMilestone.idEngineer, lastMilestone.StartDate, lastMilestone.scheduleDate, _dal.EndDateToProject, lastMilestone.ActualEndDate));
        var listOfUpdatedDep = _dal.Dependence.ReadAll().ToList();
        SetDeadLineDateForTask(lastMilestone!.IdNumberTask, firstMilestone!.IdNumberTask, listOfUpdatedDep);//Determining an end date for the project and saving it for the last task after calculating all the times of all existing tasks
        SetStartDateForTask(firstMilestone.IdNumberTask, lastMilestone.IdNumberTask, listOfUpdatedDep);//Determining a start date for the project and saving it for the first task after calculating all the times of all the existing tasks

    }
    /// <summary>
    /// A function that calculates completion times for each of the tasks in the project
    /// </summary>
    private void SetDeadLineDateForTask(int? idOfTask, int idOfStartMilestone, List<DO.Dependence?> dependenciesList)
    {
        //Stop condition
        if (idOfTask == idOfStartMilestone)
            return;
        //The data of current checked task
        DO.Task? dependentTask = _dal.Task.Read(idOfTask ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

        var DependsOnTaskList = dependenciesList.Where(dep => dep?.DependentTask == idOfTask)
            .Select(dep => dep?.DependsOnTask).ToList();
        foreach (int? taskId in DependsOnTaskList)
        {
            DO.Task currentTask = _dal.Task.Read(taskId) ?? throw new BO.BlNullPropertyException("id Of Task can't be null");
            DateTime? deadlineTime = dependentTask.LastEndDate - dependentTask.RequiredEffortTime;
            //If there is a milestone that depends on 2 tasks, determining its start time according to the longer time of the 2 tasks
            if (currentTask.Milestone == true && (currentTask.LastEndDate > deadlineTime || currentTask is null))
            {
                _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, currentTask.scheduleDate, deadlineTime, null));

            }
            else
                _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, currentTask.scheduleDate, deadlineTime, null));
            SetDeadLineDateForTask(taskId, idOfStartMilestone, dependenciesList);//A call in recursion to each of the tasks in the list to calculate its completion time according to the algorithm
        }
    }
    /// <summary>
    /// A function that calculates for each task its start date according to calculations of all the dates in the project
    /// </summary>
    private void SetStartDateForTask(int? idTask, int idOfEndMilestone, List<DO.Dependence?> dependenciesList)
    {
        if (idTask == idOfEndMilestone)
            return;

        //The data of current checked task
        DO.Task? dependentTask = _dal.Task.Read(idTask ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

        var DependentTaskList = dependenciesList.Where(dep => dep?.DependsOnTask == idTask)
            .Select(dep => dep?.DependentTask).ToList();

        foreach (int? taskId in DependentTaskList)
        {
            DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));
            DateTime? ScheduleTime = dependentTask!.scheduleDate + dependentTask.RequiredEffortTime;
            if (dependentTask?.LastEndDate + currentTask!.RequiredEffortTime > currentTask.LastEndDate)
                throw new BlPlanningOfProjectTimesException($"According to the date restrictions, the task {taskId} does not have time to be completed in its entirety");
            if (currentTask.Milestone && (currentTask.scheduleDate is null || currentTask.scheduleDate < ScheduleTime))
                _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, ScheduleTime, currentTask.LastEndDate, null));
            else
                _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, ScheduleTime, currentTask.LastEndDate, null));
            SetStartDateForTask(taskId, idOfEndMilestone, dependenciesList);

        }
    }
    /// <summary>
    /// Returning one milestone, identification by ID number
    /// </summary>
    public BO.Milestone Read(int idMilestone)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(idMilestone);
            IEnumerable<DO.Dependence> depDalList = _dal.Dependence.ReadAll()!;
            var depOnthisMilestone = (from dep in depDalList
                                      where (dep.DependentTask == idMilestone)
                                      select dep.IdNumberDependence).ToList();
            //Creating a new BO list of dependencies
            List<BO.TaskInList> depList = new List<BO.TaskInList>();

            if (depOnthisMilestone != null)
            {
                foreach (var dep in depOnthisMilestone)
                {
                    DO.Dependence? thisdep = _dal.Dependence.Read(dep);
                    DO.Task? thisTask= _dal.Task.Read(thisdep.DependsOnTask);
                    BO.status status = (BO.status)(thisTask!.scheduleDate is null ? 0
                                               : doTask!.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3);
                    depList.Add(new BO.TaskInList(thisTask!.IdNumberTask, thisTask.Description, thisTask.Alias, status));
                };
            }
            //Returning the entity with all parameters of BO type
            return new BO.Milestone
            {
                IDMilestone = doTask!.IdNumberTask,
                Description = doTask.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CreatedAtDate,
                Status = (BO.status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                DeadlineDate = doTask.LastEndDate,
                CompleteDate = doTask.ActualEndDate,
                CompletionPercentage = Convert.ToDouble((DateTime.Today - doTask.StartDate)),
                Remarks = doTask.Notes,
                Dependencies = depList
            };
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={idMilestone} does Not exist", ex);
        }
    }
    /// <summary>
    /// Milestone update like update of DO dependencies
    /// </summary>
    public void Update(BO.Milestone milestone)
    {
        DO.Task dotask = new DO.Task { 
            IdNumberTask=milestone.IDMilestone,
            Description=milestone.Description,
            Alias=milestone.Alias,
            CreatedAtDate=milestone.CreatedAtDate,
            RequiredEffortTime=null,
            Milestone=true,
            Product=null,
            Notes=milestone.Remarks,
            Level=(DO.Difficulty)1,
            idEngineer=null,
            StartDate=null,
            scheduleDate=null,
            LastEndDate=milestone.DeadlineDate,
            ActualEndDate=milestone.CompleteDate
        };
        try
        {
            _dal.Task.Update(dotask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={dotask.IdNumberTask} is not exists", ex);
        }
    }
}

