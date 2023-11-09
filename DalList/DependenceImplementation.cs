namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
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
        if ((DataSource.Dependences.Find(eng => eng.IdNumberDependence == newId) == null))
        {
            Dependence newItemWithId = new Dependence(newId,item.DependentTask,item.DependsOnTask);
            DataSource.Dependences.Add(newItemWithId);
        }
        else { throw new Exception($"Engineer with Id: {item.IdNumberDependence} is already exist"); }
        return item.IdNumberDependence;
    }

    public void Delete(int id)
    {
        Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault(dpt => dpt.IdNumberDependence == id);
        if (dependenceFound == null) { throw new Exception($"Dependence with Id: {id} don't exist"); }
        DataSource.Dependences.Remove(dependenceFound);
    }

    public Dependence? Read(int id)
    {
        Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault(dpt => dpt.IdNumberDependence == id);
        if (dependenceFound == null) { return null; }
        return dependenceFound;
    }

    public List<Dependence> ReadAll()
    {
        return new List<Dependence>(DataSource.Dependences);
    }

    public void Update(Dependence item)
    {
        Delete(item.IdNumberDependence);
        Create(item);
    }
}
