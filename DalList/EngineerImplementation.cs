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
        if ((DataSource.Engineers.Find(eng => eng.IdNumberEngineer == item.IdNumberEngineer) == null)) { 
            DataSource.Engineers.Add(item); }
        //Engineer? engineerFound = DataSource.Engineers.Find(eng => eng.IdNumberEngineer == item.IdNumberEngineer);
       else { throw new Exception($"Engineer with Id: {item.IdNumberEngineer} is already exist"); }
        return item.IdNumberEngineer;
    }

    public void Delete(int id)
    {
        Engineer engineerFound = DataSource.Engineers.FirstOrDefault(eng => eng.IdNumberEngineer == id);
        if (engineerFound == null) { throw new Exception($"Engineer with Id: {id} don't exist"); }
        DataSource.Engineers.Remove(engineerFound);
        return;
    }

    public Engineer? Read(int id)
    {

        if (DataSource.Engineers.Count>=1) 
        {
            Engineer engineerFound = DataSource.Engineers.FirstOrDefault((eng) => eng.IdNumberEngineer == id);
            if (engineerFound == null) { return null; }
            return engineerFound;
        }
        else { return null; };
       
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
