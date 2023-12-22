using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public Milestone Create(int IDMilestone)
    {

    }

    public Milestone CreateProjectSchedule(List<DO.Task> tasks, List<DO.Dependence> dependencies)
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
        foreach (IOrderedEnumerable<int> depTasks in listWithoutDuplicetes)
        {
            int milestoneId = _dal.Task.Create(new DO.Task(0, " $M1", "description", DateTime.Now, TimeSpan.Zero, true));
            foreach (var dep in listOfGroupDependencies)
            {
                if (dep.Value == depTasks)
                    newDepList.Add(new DO.Dependence(config, dep.KeyDep, milestoneId));
            }
            foreach (var task in depTasks)
            {
                newDepList.Add(new DO.Dependence(config, milestoneId, task));
            }
            int idOfFirstMilestone = _dal.Task.Create(new DO.Task(0, "start", "description", DateTime.Now, TimeSpan.Zero, true));

            IEnumerable<Dependence> depDalList = _dal.Dependence.ReadAll()!;
            foreach (var dep in depDalList)
            {
                if (dep.DependsOnTask is null)
                    newDepList.Add(new DO.Dependence ( dep.IdNumberDependence, dep.DependentTask, idOfFirstMilestone)); }
            int idOfLastMilestone = _dal.Task.Create(new DO.Task(0, "end", "description", DateTime.Now, TimeSpan.Zero, true));

            foreach (var dep in depDalList)
            {
                if (dep.DependentTask is null)
                    newDepList.Add(new DO.Dependence(dep.IdNumberDependence, idOfLastMilestone, dep.DependentTask)); }
        }
    }
    public BO.Milestone Read(int idMilestone)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(idMilestone);
            IEnumerable<Dependence> depDalList;//רשימה של תליות מעודכנת עם אבני דרך
            List<BO.TaskInList> depOnthisMilestone;//מי שתלוי באבן דרך הזו
            foreach (var dep in depDalList)
            {
                if (dep.DependsOnTask == idMilestone)
                {
                    DO.Task? thisTask = _dal.Task.Read(dep.IdNumberDependence);
                    BO.status s = (BO.status)(thisTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3);
                    depOnthisMilestone.Add(new BO.TaskInList(thisTask.IdNumberTask, thisTask.Description, thisTask.Alias, s));
                }
            }
            return new BO.Milestone
            {
                IDMilestone = doTask.IdNumberTask,
                Description = doTask.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CreatedAtDate,
                Status = (BO.status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),
                ForecastDate = DateTime.Now,//לשנות!!!
                DeadlineDate = doTask.LastEndDate,
                CompleteDate = doTask.ActualEndDate,
                CompletionPercentage = 0.0,//לשנות!!!
                Remarks = doTask.Notes,
                Dependencies = depOnthisMilestone
            };
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={idMilestone} does Not exist", ex);
        }
    }
    public Milestone Update(BO.Milestone milestone)//לתקן ממששש
    {
        DO.Task dotask = new DO.Task
         (milestone.IDMilestone,
         milestone.Alias!,
         milestone.Description!,
         milestone.CreatedAtDate,
        TimeSpan.Zero,//לחשב
         true,
         "",
         milestone.Remarks,
         (DO.Difficulty)0,
         0,
         milestone,//
         item.SchedualStartDate,//
         milestone.DeadlineDate,
         milestone.CompleteDate
         );
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
