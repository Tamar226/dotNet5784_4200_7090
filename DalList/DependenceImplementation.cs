namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependenceImplementation : IDependence
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
            Dependence newItemWithId = new Dependence(newId, item.DependentTask,item.DependsOnTask);
            DataSource.Dependences.Add(newItemWithId);
        }
        else { throw new DalAlreadyExistsException($"{item.GetType} with Id: {item.IdNumberDependence} is already exist"); }
        return item.IdNumberDependence;
    }

    public void Delete(int id)
    {
        Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault(dpt => dpt.IdNumberDependence == id);
        if (dependenceFound == null) { throw new DalDoesNotExistException($"Dependence with Id: {id} don't exist"); }
        DataSource.Dependences.Remove(dependenceFound);
    }

    public Dependence? Read(int id)
    {
        if (DataSource.Dependences.Count >= 1)
        {
            Dependence? dependenceFound = DataSource.Dependences.FirstOrDefault((dep) => dep.IdNumberDependence == id);
            if (dependenceFound == null) { return null; }
            return dependenceFound;
        }
        else { return null; };
    }

    public List<Dependence> ReadAll()
    {
        return new List<Dependence>(DataSource.Dependences);
    }

    public void Update(Dependence item)
    {
        Dependence? tempDependence = (DataSource.Dependences.Find(element => element!.IdNumberDependence == item.IdNumberDependence));
        if (tempDependence is null)
            throw new DalDoesNotExistException("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Dependences.Remove(tempDependence);
            DataSource.Dependences.Add(item);
        }
    }
  
}
