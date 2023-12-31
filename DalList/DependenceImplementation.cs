namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependenceImplementation : IDependence
{
    /// <summary>
    /// Creating a new dependent task, according to the existing tasks in the database and the ID number of each one
    /// </summary>
    public int Create(Dependence item)
    {
        int newId = 0;
        if (item.IdNumberDependence == 0)
        {
            newId = DataSource.Config.IDNextNumberTask;
        }
        else
        {
            newId = item.IdNumberDependence;
        }
        if ((DataSource.Dependences.FirstOrDefault(eng => eng.IdNumberDependence == newId) == null))
        {
            Dependence newItemWithId = new Dependence(newId, item.DependentTask,item.DependsOnTask);
            DataSource.Dependences.Add(newItemWithId);
        }
        else { throw new DalAlreadyExistsException($"Dependence with Id: {item.IdNumberDependence} is already exist"); }
        return item.IdNumberDependence;
    }
    /// <summary>
    /// Delition a new dependent task, according to the existing tasks in the database and the ID number of each one
    /// </summary>
    public void Delete(int id)
    {
        Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault(dpt => dpt.IdNumberDependence == id);
        if (dependenceFound == null) { throw new DalDeletionImpossible($"Dependence with Id: {id} don't exist"); }
        DataSource.Dependences.Remove(dependenceFound);
    }
    /// <summary>
    /// Returns one particular dependent at the user's request by ID number
    /// </summary>
    public Dependence? Read(int? id)
    {
        if (DataSource.Dependences.Count >= 1)
        {
            Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault((dep) => dep.IdNumberDependence == id);
            if (dependenceFound == null) { return null; }
            return dependenceFound;
        }
        else { return null; };
    }
    /// <summary>
    /// Returns the entire list of dependencies that exist in the repository
    /// </summary>
    public IEnumerable<Dependence> ReadAll(Func<Dependence, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependences
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependences
               select item;
    }
    /// <summary>
    /// Updating a certain pending task at the user's request
    /// </summary>
    public void Update(Dependence item)
    {
        Dependence? tempDependence = (DataSource.Dependences.First(element => element!.IdNumberDependence == item.IdNumberDependence));
        if (tempDependence is null)
            throw new DalDoesNotExistException("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Dependences.Remove(tempDependence);
            DataSource.Dependences.Add(item);
        }
    }
    /// <summary>
    /// Read a dependency by a filter
    /// </summary>
    public Dependence? Read(Func<Dependence, bool> filter)//stage 2
    {
        if (filter != null)
        {
            var foundDependence = from item in DataSource.Dependences
                    where filter(item)
                    select item;
            return foundDependence.ElementAt(0);
        }
        throw new DalNoFilterToQuery("no filther to query");
       
    }
    /// <summary>
    /// clear all the dependencies's list from the data in DO
    /// </summary>
    public void Reset()
    {
        DataSource.Dependences.Clear();
    }
}
