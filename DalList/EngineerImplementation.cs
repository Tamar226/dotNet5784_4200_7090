namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    /// <summary>
    /// 
    /// </summary>
    public int Create(Engineer item)
    {
        Engineer? engineerFound = DataSource.Engineers.Find(eng => eng.IdNumberEngineer == item.IdNumberEngineer);
        if (engineerFound != null) { throw new Exception($"Engineer with Id: {item.IdNumberEngineer} is already exist"); }
        DataSource.Engineers.Add(engineerFound);
        return item.IdNumberEngineer;
    }

    public void Delete(int id)
    {
        Engineer? engineerFound = DataSource.Engineers.Find(eng => eng.IdNumberEngineer == id);
        if (engineerFound == null) { throw new Exception($"Engineer with Id: {id} don't exist"); }
        DataSource.Engineers.Remove(engineerFound);
        return;
    }

    public Engineer? Read(int id)
    {
        Engineer? engineerFound = DataSource.Engineers.Find(eng => eng.IdNumberEngineer == id);
        if (engineerFound == null) { return null; }
        return engineerFound;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? tempEngineer = (DataSource.Engineers.Find(element => element!.IdNumberEngineer == item.IdNumberEngineer));
        if (tempEngineer is null)
            throw new Exception("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Engineers.Remove(tempEngineer);
            DataSource.Engineers.Add(item);
        }
    }
}
