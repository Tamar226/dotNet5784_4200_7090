namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public IEnumerable<BO.Task> ReadAll();
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    public BO.Task Read(int idTask);
    public void Delete(int id);
    public void Update(BO.Task item);


}
