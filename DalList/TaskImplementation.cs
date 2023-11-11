﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    //Creates a new object.
    //Checks if the received identity number is in the list, and updates the new object with the received number
    //+ all the values created for it in INTALIZATION.
    public int Create(Task item)
    {
        int newId = 0;
        int idEng = 0;
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
        if ((DataSource.Tasks.Find(eng => eng.IdNumberTask == newId) == null))
        {
            Task newItemWithId = new Task(newId, item.Description, item.Nickname, false, item.Product, item.Notes, item.Level, idEng, item.CreationDate, item.StartDate, item.foresastdate, item.LastEndDate, null);
            DataSource.Tasks.Add(newItemWithId);
        }
        else { throw new DalAlreadyExistsException($"{item.GetType} with Id: {item.IdNumberTask} is already exist"); }
        return item.IdNumberTask;

    }
    public void Delete(int id)
    {
        Task? taskFound = DataSource.Tasks.FirstOrDefault(tsk => tsk.IdNumberTask == id);
        if (taskFound == null) { throw new DalDoesNotExistException($"Task with Id: {id} don't exist"); }
        DataSource.Tasks.Remove(taskFound);
    }

    public Task? Read(int id)
    {
        if (DataSource.Tasks.Count >= 1)
        {
            Task? taskFound = DataSource.Tasks.FirstOrDefault((eng) => eng.IdNumberTask == id);
            if (taskFound == null) { return null; }
            return taskFound;
        }
        else { return null; };
    }

    public List<Task> ReadAll()
    {

        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? tempTask = (DataSource.Tasks.Find(element => element!.IdNumberTask == item.IdNumberTask));
        if (tempTask is null)
            throw new DalDoesNotExistException("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Tasks.Remove(tempTask);
            DataSource.Tasks.Add(item);
        }
    }
  
}
