using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal =Factory.Get;
    /// <summary>
    /// בקשת רשימת משימות
    /// </summary>
    public IEnumerable<TaskInList> ReadAll()
    {
        throw new NotImplementedException();
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
