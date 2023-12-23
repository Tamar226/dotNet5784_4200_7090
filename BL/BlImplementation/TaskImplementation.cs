using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal =Factory.Get;
    /// <summary>
    /// Create new Task -return a new BO.Task.
    /// </summary>
    public int Create(BO.Task item)
    {
        DO.Task dotask = new DO.Task
          (item.IdTask,
          item.Description!,
          item.Alias!,
          item.CreatedAtDate,
         TimeSpan.Zero,//לחשב
          item.Milestone is null ? false : true,
          item.Deliverables,
          item.Remarks,
          (DO.Difficulty)item.CopmlexityLevel,
          item.Engineer!.Id,
          item.StartDate,
          item.SchedualStartDate,
          item.DeadlineDate,
          item.CompleteDate
          );
        try
        {
            int idTask = _dal.Task.Create(dotask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={dotask.IdNumberTask} already exists", ex);
        }
    }
    /// <summary>
    /// Get all tasks in the data
    /// </summary>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter)//לתקן שגיאה אחת
    {
        var taskList = (from DO.Task doTask in _dal.Task.ReadAll()
                        where doTask.Milestone == false
                        select (new BO.TaskInList(doTask.IdNumberTask, doTask.Description, doTask.Alias, (BO.status)(doTask.scheduleDate is null ? 0
                                                       : doTask.StartDate is null ? 1
                                                       : doTask.ActualEndDate is null ? 2
                                                       : 3))));
        if(filter != null)
        {
            var taskListWhithFilter = (from item in taskList
                                       where filter(item)
                                       select item);
            return taskListWhithFilter!;
        }
        else
        {
            return taskList!;
        }
    }
    /// <summary>
    /// Read spesipic task by id
    /// </summary>
    public BO.Task Read(int idTask)//לתקן 2 שגיאות
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(idTask);
            return new BO.Task
            {
                IdTask = doTask!.IdNumberTask,
                Description = doTask.Description,
                Alias = doTask.Alias,
                //Initialize the milestone utility
                Milestone = new BO.MilestoneInTask()
                {
                    Id = _dal.Task!.Read(_dal.Dependence!.Read(dep =>
                    {
                        _dal.Task!.Read(task => task.Milestone && task.IdNumberTask == dep.DependentTask);
                        return dep.DependsOnTask == doTask.IdNumberTask;
                    })!.DependentTask)!.IdNumberTask,

                    Alias = _dal.Task!.Read(_dal.Dependence!.Read(dep =>
                    {
                        _dal.Task!.Read(task => task.Milestone && task.IdNumberTask == dep.DependentTask);
                        return dep.DependsOnTask == doTask.IdNumberTask;
                    })!.DependentTask)?.Alias
                },
                CreatedAtDate = doTask.CreatedAtDate,
                Status = (BO.status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),

                //BaselineStartDate = doTask.//קשור למילסטון
                StartDate = doTask.StartDate,
                SchedualStartDate = doTask.scheduleDate,
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                DeadlineDate = doTask.LastEndDate,
                CompleteDate = doTask.ActualEndDate,
                Deliverables = doTask.Product,
                Remarks = doTask.Notes,

                //Initialize the engineer utility entity
                Engineer = new EngineerInTask
                {
                    Id = doTask.idEngineer!,
                    Name = _dal.Engineer?.Read((int)doTask.idEngineer!)!.Name!
                },
                CopmlexityLevel = (BO.EngineerExperience)doTask.Level,
            };
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={idTask} does Not exist", ex);
        }
    }
    
    /// <summary>
    /// Delete task
    /// </summary>
    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch { throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); }
    }
 
   /// <summary>
   /// update specific task
   /// </summary>
    public void Update(BO.Task item)
    {
        DO.Task dotask = new DO.Task
         (item.IdTask,
         item.Description!,
         item.Alias!,
         item.CreatedAtDate,
        TimeSpan.Zero,//לחשב
         item.Milestone is null ? false : true,
         item.Deliverables,
         item.Remarks,
         (DO.Difficulty)item.CopmlexityLevel,
         item.Engineer!.Id,
         item.StartDate,
         item.SchedualStartDate,
         item.DeadlineDate,
         item.CompleteDate
         );
        try
        {
            _dal.Task.Update(dotask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={dotask.IdNumberTask} is not exists", ex);
        }
    }
}
