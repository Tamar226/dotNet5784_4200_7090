using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal =Factory.Get;
    /// <summary>
    /// בקשת רשימת משימות
    /// </summary>
    public IEnumerable<BO.TaskInList> ReadAll()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                where doTask.Milestone == false
                select new BO.Task
                {
                    IdTask = doTask.IdNumberTask,
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    //Milestone = doTask.Milestone is true ? new BO.MilestoneInTask() : null,
                    CreatedAtDate = doTask.CreatedAtDate,
                    //status
                    //BaselineStartDate = doTask.
                    StartDate = doTask.StartDate,
                    //SchedualStartDate
                    ForecastDate = doTask.foresastdate,
                    DeadlineDate = doTask.LastEndDate,
                    CompleteDate = doTask.ActualEndDate,
                    Deliverables = doTask.Product,
                    Remarks = doTask.Notes,
                    Engineer = doTask.idEngineer is not 0 ? new BO.EngineerInTask() : NULL,
                    CopmlexityLevel = (BO.EngineerExperience)doTask.Level,
                }); ;
    }
    /// <summary>
    /// בקשת פרטי משימה
    /// </summary>
    public BO.Task ReadSpecipicTask(int idTask)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// הוספת משימה
    /// </summary>

    public int Create(BO.Task item)
    {
        DO.Task dotask = new DO.Task
          (item.IdTask,item.Description,item.Alias,item.CreatedAtDate,item.Status,item.Milestone,item.BaselineStartDate,item.StartDate,item.SchedualStartDate);
        try
        {
            int idStud = _dal.Student.Create(doStudent);
            return idStud;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }

    }
    /// <summary>
    /// מחיקת משימה
    /// </summary>
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// מחזיק אובייקט משימה מסוג מסוים
    /// </summary>
    public IEnumerable<TaskInEngineer> GetTasksByFilter()
    {
        throw new NotImplementedException();
    }
   /// <summary>
   /// עדכון
   /// </summary>
    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
