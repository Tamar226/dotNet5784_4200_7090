namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates a new object.
    ///Checks if the received identity number is in the list, and updates the new object with the received number
    ///+ all the values created for it in INTALIZATION.
    /// </summary>
    public int Create(Task item)
    {
        int newId = 0;
        int? idEng = 0;
        if (item.idEngineer != 0)
        {
            idEng = item.idEngineer;
        }
        if (item.IdNumberTask == 0)
        {
            newId = DataSource.Config.IDNextNumberTask;
        }
        else
        {
            newId = item.IdNumberTask;
        }
        if ((DataSource.Tasks.FirstOrDefault(eng => eng.IdNumberTask == newId) == null))
        {
            Task newItemWithId = new Task(newId, item.Description, item.Alias, item.CreatedAtDate, item.RequiredEffortTime, false, item.Product, item.Notes, item.Level, idEng,  item.StartDate, item.scheduleDate, item.LastEndDate, null);
            DataSource.Tasks.Add(newItemWithId);
        }
        else { throw new DalAlreadyExistsException($"{item.GetType} with Id: {item.IdNumberTask} is already exist"); }
        return item.IdNumberTask;

    }
    /// <summary>
    /// Deleting a task selected by the user by ID number in the XML file
    /// </summary>
    public void Delete(int id)
    {
        Task? taskFound = DataSource.Tasks.FirstOrDefault(tsk => tsk.IdNumberTask == id);
        if (taskFound == null) { throw new DalDeletionImpossible($"Task with Id: {id} don't exist"); }
        DataSource.Tasks.Remove(taskFound);
    }
    /// <summary>
    /// Finding the task selected by the user in the XML file and returning it by ID number (by queries) 
    /// </summary>
    public Task? Read(int? id)
    {
        if (DataSource.Tasks.Count >= 1)
        {
            Task? taskFound = DataSource.Tasks.FirstOrDefault((eng) => eng.IdNumberTask == id);
            if (taskFound == null) { return null; }
            return taskFound;
        }
        else { return null; };
    }
    /// <summary>
    /// Finding all tasks in the XML file and returning them (by queries) 
    /// </summary>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }
    /// <summary>
    ///  Updating a task selected by the user by ID number and updating the required attributes in the XML file
    /// </summary>
    public void Update(Task item)
    {
        Task? tempTask = (DataSource.Tasks.FirstOrDefault(element => element!.IdNumberTask == item.IdNumberTask));
        if (tempTask is null)
            throw new DalDoesNotExistException("An object of type Task with such an ID does not exist");
        else
        {
            DataSource.Tasks.Remove(tempTask);
            DataSource.Tasks.Add(item);
        }
    }
    /// <summary>
    /// Finding the task selected by the user in the XML file and returning it by ID number (by queries and filter) 
    /// </summary>
    public Task? Read(Func<Task, bool> filter)//stage 2
    {
        if (filter != null)
        {
            var foundDependence = from item in DataSource.Tasks
                                  where filter(item)
                                  select item;
            return foundDependence.ElementAt(0);
        }
        throw new DalNoFilterToQuery("no filther to query");

    }
    /// <summary>
    /// Clear the tasks list in XML file
    /// </summary>
    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
}
