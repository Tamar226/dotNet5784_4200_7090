namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates a new object.
    ///Checks if the received identity number is in the list, and updates the new object with the received number
    ///+ all the values created for it in INTALIZATION.
    /// </summary>
    public int Create(Engineer item)
    {
        if ((DataSource.Engineers.FirstOrDefault(eng => eng.IdNumberEngineer == item.IdNumberEngineer) == null)) { 
            DataSource.Engineers.Add(item); }
       else { throw new DalAlreadyExistsException($"Engineer with Id: {item.IdNumberEngineer} is already exist"); }
        return item.IdNumberEngineer;
    }
    /// <summary>
    /// Deleting an engineer from the list after they found him by ID
    /// </summary>
    public void Delete(int id)
    {
        Engineer? engineerFound = DataSource.Engineers.FirstOrDefault(eng => eng.IdNumberEngineer == id);
        if (engineerFound == null) { throw new DalDeletionImpossible($"Engineer with Id: {id} don't exist"); }
        DataSource.Engineers.Remove(engineerFound);
        return;
    }
    /// <summary>
    /// Read 1 engineer bh his ID
    /// </summary>
    public Engineer? Read(int? id)
    {

        if (DataSource.Engineers.Count>=1) 
        {
            Engineer? engineerFound = DataSource.Engineers.FirstOrDefault(eng => eng.IdNumberEngineer == id);
            if (engineerFound == null) { return null; }
            return engineerFound;
        }
        else { return null; };
       
    }
    /// <summary>
    /// Returning the entire list of engineers
    /// </summary>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }
    /// <summary>
    /// Updating details of an engineer after finding him in the list - by ID number
    /// </summary>
    public void Update(Engineer item)
    {
        Engineer? tempEngineer = (DataSource.Engineers.FirstOrDefault(element => element!.IdNumberEngineer == item.IdNumberEngineer));
        if (tempEngineer is null)
            throw new DalDoesNotExistException("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Engineers.Remove(tempEngineer);
            DataSource.Engineers.Add(item);
        }
    }
    /// <summary>
    /// Read 1 engineer bh his ID+filter
    /// </summary>
    public Engineer? Read(Func<Engineer, bool> filter)//stage 2
    {
        if (filter != null)
        {
            var foundDependence = from item in DataSource.Engineers
                                  where filter(item)
                                  select item;
            return foundDependence.ElementAt(0);
        }
        throw new DalNoFilterToQuery("no filther to query");
    }
    /// <summary>
    /// Clear the enguneers's list from the data
    /// </summary>
    public void Reset()
    {
        DataSource.Engineers.Clear();
    }

}
