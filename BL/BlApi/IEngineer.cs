using DO;

namespace BlApi;

public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll();
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? value);

    public void Delete(int id);

    public void Update(BO.Engineer item);
    
}
