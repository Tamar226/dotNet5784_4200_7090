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
    /// Add new Engineer
    /// </summary>
    public int Create(BO.Engineer item)
    {
        DO.Engineer doEngineer = new DO.Engineer
         (item.IdEngineer, item.Name, item.Email, (DO.Difficulty)item.Level, item.Cost);
        //להוסיף TASK
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
    ///<summary>
    ///get one engineer
    /// </summary>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            IdEngineer = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = findTaskInEngineer(doEngineer.IdNumberEngineer),
        };
    }
    private TaskInEngineer findTaskInEngineer(int id)
    {
        var tasks = _dal.Task.ReadAll();
        try
        {
            return new BO.TaskInEngineer(from task in tasks
                                         where task.idEngineer == id && task.StartDate != null && task.ActualEndDate == null
                                         select task.IdNumberTask
                           , Convert.ToString(from task2 in tasks
                                              where task2.idEngineer == id && task2.StartDate != null && task2.ActualEndDate == null
                                              select task2.Alias)!);
        }
        catch { return null!; };
    }
    /// <summary>
    /// Get all engineers
    /// </summary>
    public IEnumerable<BO.Engineer> ReadAll()
    {
        var tasks =_dal.Task.ReadAll() ;
 
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    IdEngineer = doEngineer.IdNumberEngineer,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = new BO.TaskInEngineer(from task in tasks
                            where task.idEngineer == doEngineer.IdNumberEngineer && task.StartDate!=null && task.ActualEndDate==null
                                                 select task.IdNumberTask
                            , Convert.ToString( from task2 in tasks
                              where task2.idEngineer == doEngineer.IdNumberEngineer && task2.StartDate != null && task2.ActualEndDate == null
                                                select task2.Alias)!),
                }) ;

    }
    /// <summary>
    /// Delete engineer
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
    /// update engineer
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
