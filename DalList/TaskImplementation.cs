namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.ID_next_number_Task;
        // item -להעתיק את המספר החדש ל
        return newId;
        
    }

    public void Delete(int id)
    {
        if (!DataSource.Tasks.Find(tsk => (tsk.Id_number_Task) == id))
            throw 'we dont have this... '
        DataSource.Tasks.Remove(tsk => (tsk.Id_number_Task) == id)
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
