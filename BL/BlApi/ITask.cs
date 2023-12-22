namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter);
    public BO.Task Read(int idTask);
    public void Delete(int id);
    public void Update(BO.Task item);


}
