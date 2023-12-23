namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
/// <summary>
/// CRUD functions of Task
/// </summary>
internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newId = 0;
        int? idEng = 0;
        if (item.idEngineer != 0)
        {
            idEng = item.idEngineer;
        }
        if (item.IdNumberTask == 0)
        {
            newId = Config.NextTaskId;
        }
        else
        {
            newId = item.IdNumberTask;
        }
        if ((tasks.FirstOrDefault(eng => eng.IdNumberTask == newId) == null))
        {
            Task newItemWithId = new Task(newId, item.Alias, item.Description, item.CreatedAtDate, item.RequiredEffortTime, false, item.Product, item.Notes, item.Level, idEng,  item.StartDate, item.scheduleDate, item.LastEndDate, null);
            tasks.Add(newItemWithId);
        }
        else { throw new DalAlreadyExistsException($"{item.GetType} with Id: {item.IdNumberTask} is already exist"); }
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
        return item.IdNumberTask;

    }

    public void Delete(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newId = 0;
        Task? taskFound=tasks.FirstOrDefault(tsk => tsk.IdNumberTask == id);
        if (taskFound == null) { throw new DalDeletionImpossible($"Task with Id: {id} don't exist"); }
        tasks.Remove(taskFound);
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
    }

    public Task? Read(int? id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (tasks.Count >= 1)
        {
            Task? taskFound = tasks.FirstOrDefault((eng) => eng.IdNumberTask == id);
            if (taskFound == null) { return null; }
            return taskFound;
        }
        else { return null; };
            
    }
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter != null)
        {
            var foundDependence = from item in tasks
                                  where filter(item)
                                  select item;
            return foundDependence.ElementAt(0);
        }
        throw new DalNoFilterToQuery("no filther to query");
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter != null)
        {
            return from item in tasks
                   where filter(item)
                   select item;
        }
        return from item in tasks
               select item;
    }
    public void Update(Task item)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? tempTask = (tasks.FirstOrDefault(element => element!.IdNumberTask == item.IdNumberTask));
        if (tempTask is null)
            throw new DalDoesNotExistException("An object of type Task with such an ID does not exist");
        else
        {
            tasks.Remove(tempTask);
            tasks.Add(item);
        }
        XMLTools.SaveListToXMLSerializer(tasks, "tasks");
    }
    public void Reset()
    {
        XMLTools.ResetFile("tasks");
    }
}
