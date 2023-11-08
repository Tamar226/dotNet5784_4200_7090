namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {

        int newId = DataSource.Config.IDNextNumberDependence;
        Dependence newItem = new Dependence();
        DataSource.Dependences.Add(newItem);
        return newId;
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
