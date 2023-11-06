namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.ID_next_number_Task;
        Task newItem= new Task();
        DataSource.Tasks.Add(newItem);
        return newId;
        
    }

    public void Delete(int id)
    {
        Task? taskFound = DataSource.Tasks.Find(tsk => tsk.IdNumberTask == id);
        if (taskFound != null) { throw new Exception($"Task with Id={id} don't exist"); }
        DataSource.Tasks.Remove(taskFound);
    }


    public Task? Read(int id)
    {
        Task? taskFound = DataSource.Tasks.Find(tsk => tsk.IdNumberTask == id);
        if (taskFound == null) { return null; }
        return taskFound;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Delete(item.IdNumberTask);
        Create(item);
    }
}
