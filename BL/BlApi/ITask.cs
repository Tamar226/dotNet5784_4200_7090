namespace BlApi;

public interface ITask
{
    public BO.Task Create( int idTask);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll();
    public void Update(BO.Task item);
    public void Delete(int id);
    public BO.TaskInEngineer GetDetailedTaskForEngineer(int IdTask, int IdEngineer);
    public IEnumerable<BO.TaskInEngineer> GetTasksByFilter();
    public int ReturnSpecipicTask(BO.Task item);


}
