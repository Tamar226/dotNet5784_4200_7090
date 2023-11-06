namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {

        int newId = DataSource.Config.Id_number_Dependence;
        // item -להעתיק את המספר החדש ל
        return newId;
    }

    public void Delete(int id)
    {
        DataSource.Tasks.Remove(m => (m.Id_number_Dependence) == id);
    }

    public Dependence? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Dependence> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependence item)
    {
        throw new NotImplementedException();
    }
}
