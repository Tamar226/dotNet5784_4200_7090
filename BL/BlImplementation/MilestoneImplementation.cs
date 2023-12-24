using BlApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public void CreateProjectSchedule(List<DO.Task> tasks, List<DO.Dependence> dependencies)
    {
        DateTime dateTimeProject = DateTime.Now;
        var listOfGroupDependencies = from dep in dependencies
                                      where dep.DependentTask is not null && dep.DependsOnTask is not null
                                      group dep by dep.DependentTask into newGroup
                                      let depList = (from dep in newGroup
                                                     select dep.DependsOnTask).Order()
                                      select new { KeyDep = newGroup.Key, Value = depList };
        var listWithoutDuplicetes = (from dep in listOfGroupDependencies
                                     select dep.Value).Distinct();

        List<DO.Dependence> newDepList = new List<DO.Dependence>();
        _dal.Dependence.Reset();

        DO.Task firstMilestone = _dal.Task.Read(tasks.Where(task => task!.Alias == "Start").Select(task => task!.IdNumberTask).First())!;
        DO.Task lastMilestone = _dal.Task.Read(tasks.Where(task => task!.Alias == "End").Select(task => task!.IdNumberTask).First())!;
        int runningNameForMilestone=0;
        foreach (IOrderedEnumerable<int> depTasks in listWithoutDuplicetes)
        {
            
            int milestoneId = _dal.Task.Create(new DO.Task(0, "M"+runningNameForMilestone, "description", DateTime.Now, TimeSpan.Zero, true));
            foreach (var dep in listOfGroupDependencies)
            {
                if (dep.Value == depTasks)
                {
                    int id = _dal.Dependence.Create(new DO.Dependence(0, dep.KeyDep, milestoneId));
                    newDepList.Add(_dal.Dependence.Read(id)!);
                }
                    
            }
            foreach (var task in depTasks)
            {
                int id=_dal.Dependence.Create(new DO.Dependence(0, milestoneId, task));
                newDepList.Add(_dal.Dependence.Read(id)!);
            }

            foreach (var dep in dependencies)
            {
                if (dep.DependsOnTask is null)
                    _dal.Dependence.Create(new DO.Dependence(dep.IdNumberDependence, dep.DependentTask, firstMilestone.IdNumberTask));
                    newDepList.Add(new DO.Dependence ( dep.IdNumberDependence, dep.DependentTask, firstMilestone.IdNumberTask)); 
            }

            foreach (var dep in dependencies)
            {
                if (dep.DependentTask is null)
                    _dal.Dependence.Create(new DO.Dependence(dep.IdNumberDependence, lastMilestone.IdNumberTask, dep.DependentTask));
                   newDepList.Add(new DO.Dependence(dep.IdNumberDependence, lastMilestone.IdNumberTask, dep.DependentTask));
            }
            runningNameForMilestone++;
        }
        SetDeadLineDateForTask(lastMilestone.IdNumberTask, firstMilestone.IdNumberTask, newDepList!);
        SetDeadScheduleForTask(firstMilestone.IdNumberTask, lastMilestone.IdNumberTask, newDepList!);

    }
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
            DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

            if (currentTask is null)
                throw new BO.BlDoesNotExistException($"task with id {taskId} is not exist");

            DateTime? deadLineDate = dependentTask?.LastEndDate - dependentTask?.RequiredEffortTime;

            if (currentTask.Milestone is true && (currentTask.LastEndDate is null || deadLineDate < currentTask.LastEndDate))
            {
                    _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, currentTask.scheduleDate, deadLineDate, null));
            }
            else
                _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, currentTask.scheduleDate, deadLineDate, null));

            SetDeadLineDateForTask(taskId, idOfStartMilestone, dependenciesList);
        }

    }
    private void SetDeadScheduleForTask(int? idOfTask, int idOfEndMilestone, List<DO.Dependence?> dependenciesList)
    {
        //Stop condition
        if (idOfTask == idOfEndMilestone)
            return;

        //The data of current checked task
        DO.Task? dependentTask = _dal.Task.Read(idOfTask ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

        var DependentTaskList = dependenciesList.Where(dep => dep?.DependsOnTask == idOfTask)
            .Select(dep => dep?.DependentTask).ToList();

        foreach (int? taskId in DependentTaskList)
        {
            DO.Task? currentTask = _dal.Task.Read(taskId ?? throw new BO.BlNullPropertyException("id Of Task can't be null"));

            if (currentTask is null)
                throw new BO.BlDoesNotExistException($"task with id {taskId} is not exist");
            if (dependentTask?.LastEndDate + currentTask.RequiredEffortTime > currentTask.LastEndDate)
                throw new BO.BlPlanningOfProjectTimesException($"According to the date restrictions, the task {taskId} does not have time to be completed in its entirety");
            _dal.Task.Update(new DO.Task(currentTask.IdNumberTask, currentTask.Alias, currentTask.Description, currentTask.CreatedAtDate, currentTask.RequiredEffortTime, currentTask.Milestone, currentTask.Product, currentTask.Notes, currentTask.Level, currentTask.idEngineer, currentTask.StartDate, dependentTask?.LastEndDate, currentTask.LastEndDate, null));


            SetDeadScheduleForTask(taskId, idOfEndMilestone, dependenciesList);
        }
    }
    public BO.Milestone Read(int idMilestone)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(idMilestone);
            IEnumerable<DO.Dependence> depDalList = _dal.Dependence.ReadAll()!;
            var depOnthisMilestone = (from dep in depDalList
                                      where (dep.DependentTask == idMilestone)
                                      select dep.IdNumberDependence).ToList();
            List<BO.TaskInList> depList= new List<BO.TaskInList>();
            foreach (var dep in depOnthisMilestone)
            {
                DO.Task? thisTask = _dal.Task.Read(dep);
                BO.status status = (BO.status)(thisTask!.scheduleDate is null ? 0
                                           : doTask!.StartDate is null ? 1
                                           : doTask.ActualEndDate is null ? 2
                                           : 3);
                depList.Add(new BO.TaskInList(thisTask.IdNumberTask, thisTask.Description, thisTask.Alias, status));
            };
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
                CompletionPercentage = Convert.ToDouble((DateTime.Today - doTask.StartDate)),//לבדוק אם זה חישוב נכון
                Remarks = doTask.Notes,
                Dependencies = depList
            };
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={idMilestone} does Not exist", ex);
        }
    }
    public void Update(BO.Milestone milestone)
    {
        DO.Task dotask = _dal.Task.Read(milestone.IDMilestone)!;
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

