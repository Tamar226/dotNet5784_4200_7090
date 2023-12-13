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
    public IEnumerable<Engineer> ReadAll()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    IdEngineer =doEngineer.IdEngineer,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = doEngineer.Level,
                    Cost = doEngineer.Cost,
                });

    }
    /// <summary>
    /// בקשת מהנדס לפי בקשה/תכונה מסויימת
    /// </summary>
    public IEnumerable<Engineer> GetEngineerByFilter()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// בקשת פרטי מהנדס אחד
    /// </summary>
    public Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        return new BO.Engineer()
        {
            IdEngineer = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = doEngineer.Level,
            Cost = doEngineer.Cost,
    
        };

    }
    /// <summary>
    /// הוספת מהנדס
    /// </summary>
    public int Create(Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer
         (item.IdEngineer, item.Name, item.Email, item.Level, item.Cost);
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
        _dal.Engineer.Delete(id);
    }
    /// <summary>
    /// עדכון מהנדס
    /// </summary>
    public void Update(BO.Engineer item)
    {
        _dal.Engineer.Update(item);

    }
}
