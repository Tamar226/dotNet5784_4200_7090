using BlApi;
using BO;
using DalApi;
using DO;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace BlImplementation;

internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    /// <summary>
    /// הוספת מהנדס
    /// </summary>
    public int Create(BO.Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer
         (item.IdEngineer, item.Name, item.Email, (DO.Difficulty)item.Level, item.Cost);//להוסיף TASK
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.IdEngineer} already exists", ex);
        }
    }
    /// <summary>
    /// בקשת רשימת מהנדסים
    /// </summary>
    ///    /// <summary>
    /// בקשת פרטי מהנדס אחד
    /// </summary>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        return new BO.Engineer()
        {
            IdEngineer = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            //להוסיף את TASK 
        };

    }

    public IEnumerable<BO.Engineer> ReadAll()
    {
        List<BO.Task> tasks = new List<BO.Task>();
 
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    IdEngineer = doEngineer.IdNumberEngineer,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = new BO.TaskInEngineer(from task in tasks
                            where task.Engineer!.Id == doEngineer.IdNumberEngineer && task.Status== (BO.status)3
                                                 select task.IdTask
                            , Convert.ToString( from task2 in tasks
                              where task2.Engineer!.Id == doEngineer.IdNumberEngineer && task2.Status == (BO.status)3
                                                select task2.Alias)!),
                }) ;

    }
    /// <summary>
    /// בקשת מהנדס לפי בקשה/תכונה מסויימת
    /// </summary>
 
    /// <summary>
    /// מחיקת מהנדס
    /// </summary>
    public void Delete(int id)
    {
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} doesn't exists", ex);
        }
    }
    /// <summary>
    /// עדכון מהנדס
    /// </summary>
    public void Update(BO.Engineer item)
    {
        _dal.Engineer.Update(new DO.Engineer()
        {
            IdNumberEngineer = item.IdEngineer,
            Name = item.Name,
            Email = item.Email,
            Level = (DO.Difficulty)item.Level,
            Cost = item.Cost
        }
        );
    }
}
