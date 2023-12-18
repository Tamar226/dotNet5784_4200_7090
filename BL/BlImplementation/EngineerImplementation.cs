using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    /// <summary>
    /// בקשת רשימת מהנדסים
    /// </summary>
    public IEnumerable<DO.Engineer> ReadAll()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    IdEngineer = doEngineer.IdNumberEngineer,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = taskEngineer != null ? new BO.TaskInEngineer { Id = taskEngineer.IdNumberTask, Alias = taskEngineer} : null
                });

    }
    /// <summary>
    /// בקשת מהנדס לפי בקשה/תכונה מסויימת
    /// </summary>
    public IEnumerable<BO.Engineer> GetEngineerByFilter()
    {
        throw new NotImplementedException();
    }
    /// <summary>
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
            Level = (BO.EngineerExperience)doEngineer.Level,//הוא מסוג שונה
            Cost = doEngineer.Cost,
        };

    }
    /// <summary>
    /// הוספת מהנדס
    /// </summary>
    public int Create(BO.Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer
         (item.IdEngineer, item.Name, item.Email, (DO.Difficulty)item.Level, item.Cost);//סוג שונה
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.IdEngineer} already exists",ex);
        }
    }
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
            Level = (DO.Difficulty)item.Level,//הוא מסוג שונה
            Cost = item.Cost
        }
        );

    }
}
