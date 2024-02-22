using BlApi;
using BO;
using System.Collections.Generic;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;
    /// <summary>
    /// Create new Task -return a new BO.Task.
    /// </summary>
    public int Create(BO.Task item)
    {
        DO.Task dotask = new DO.Task
          (item.IdTask,
          item.Description!,
          item.Alias!,
          DateTime.Now,
          TimeSpan.Zero,//לחשב
          item.Milestone is null ? false : true,
          item.Deliverables,
          item.Remarks,
          (DO.Difficulty)item.CopmlexityLevel
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
    public IEnumerable<BO.Task> ReadAll()
    {
        var taskList = (from DO.Task doTask in _dal.Task.ReadAll()
                        where doTask.Milestone == false
                        select new BO.Task
                        {
                            IdTask = doTask!.IdNumberTask,
                            Description = doTask.Description,
                            Alias = doTask.Alias,
                            Milestone = findMilestoneForTask(doTask.IdNumberTask),
                            CreatedAtDate = doTask.CreatedAtDate,
                            Status = (BO.Status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),
                            StartDate = doTask.StartDate,
                            SchedualStartDate = doTask.scheduleDate,
                            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                            DeadlineDate = doTask.LastEndDate,
                            CompleteDate = doTask.ActualEndDate,
                            Deliverables = doTask.Product,
                            Remarks = doTask.Notes,
                            Engineer = findEngineerForTask(doTask.IdNumberTask),
                            CopmlexityLevel = (BO.EngineerExperience)doTask.Level,
                        });

        List<BO.Task> boList = new List<BO.Task>();
        foreach (var task in taskList)
        {

        }
        return taskList!;
    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        var taskList = (from DO.Task doTask in _dal.Task.ReadAll()
                        where doTask.Milestone == false
                        select new BO.Task
                        {
                            IdTask = doTask!.IdNumberTask,
                            Description = doTask.Description,
                            Alias = doTask.Alias,
                            Milestone = findMilestoneForTask(doTask.IdNumberTask),
                            CreatedAtDate = doTask.CreatedAtDate,
                            Status = (BO.Status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),
                            StartDate = doTask.StartDate,
                            SchedualStartDate = doTask.scheduleDate,
                            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                            DeadlineDate = doTask.LastEndDate,
                            CompleteDate = doTask.ActualEndDate,
                            Deliverables = doTask.Product,
                            Remarks = doTask.Notes,
                            Engineer = findEngineerForTask(doTask.IdNumberTask),
                            CopmlexityLevel = (BO.EngineerExperience)doTask.Level,
                        });

        List<BO.Task> boList = new List<BO.Task>();
        foreach (var task in taskList)
        {
            if (filter!(task))
            {
                boList.Add(task);
            }
        }
        return boList!;
    }
    public BO.Task Read(int idTask)
    {
        try
        {
           DO.Task? doTask = _dal.Task.Read(idTask);
            return new BO.Task
            {
                IdTask = doTask!.IdNumberTask,
                Description = doTask.Description,
                Alias = doTask.Alias,
                Milestone= findMilestoneForTask(doTask.IdNumberTask),
                CreatedAtDate = doTask.CreatedAtDate,
                Status = (BO.Status)(doTask.scheduleDate is null ? 0
                                               : doTask.StartDate is null ? 1
                                               : doTask.ActualEndDate is null ? 2
                                               : 3),
                StartDate = doTask.StartDate,
                SchedualStartDate = doTask.scheduleDate,
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
                DeadlineDate = doTask.LastEndDate,
                CompleteDate = doTask.ActualEndDate,
                Deliverables = doTask.Product,
                Remarks = doTask.Notes,
                Engineer= findEngineerForTask(doTask.IdNumberTask),
                CopmlexityLevel = (BO.EngineerExperience)doTask.Level,
            };
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={idTask} does Not exist", ex);
        }
    }
    /// <summary>
    /// Finding one milestone for a task
    /// </summary>
    private MilestoneInTask findMilestoneForTask(int id)
    {
        try
        {
            return new BO.MilestoneInTask()
            {
                Id = _dal.Task!.Read(_dal.Dependence!.Read(dep =>
                {
                    _dal.Task!.Read(task => task.Milestone && task.IdNumberTask == dep.DependentTask);
                    return dep.DependsOnTask == id;
                })!.DependentTask)!.IdNumberTask,

                Alias = _dal.Task!.Read(_dal.Dependence!.Read(dep =>
                {
                    _dal.Task!.Read(task => task.Milestone && task.IdNumberTask == dep.DependentTask);
                    return dep.DependsOnTask == id;
                })!.DependentTask)?.Alias
            };

        }
        catch { return null!; }
    }
    /// <summary>
    /// Finding one engineer for a task
    /// </summary>
    private EngineerInTask findEngineerForTask(int id)
    {
        try
        {
            if(_dal.Engineer?.Read(id!)is null)
            {
                return null!;
            }
            BO.EngineerInTask eng = new BO.EngineerInTask
            {
                Id = id!,
                Name = _dal.Engineer?.Read(id!)!.Name!
            };
            return eng;
            ;
        }
        catch { return null!; };
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
          DateTime.Now,
          TimeSpan.Zero,
          item.Milestone is null ? false : true,
          item.Deliverables,
          item.Remarks,
          (DO.Difficulty)item.CopmlexityLevel
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
