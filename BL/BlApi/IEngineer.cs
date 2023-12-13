using DO;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll();
    public void Update(BO.Task item);
    public void Delete(int id);
    public IEnumerable<BO.Engineer> GetEngineerByFilter();
    
}
