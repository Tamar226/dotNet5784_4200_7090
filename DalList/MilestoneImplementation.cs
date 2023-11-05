namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class MilestoneImplementation : IMilestone
{
    public int Create(Milestone item)
    {

        int newId = DataSource.Config.Id_number_Milestone;
        // item -להעתיק את המספר החדש ל
        return newId;
    }

    public void Delete(int id)
    {
        DataSource.Tasks.Remove(m => (m.Id_number_Milestone) == id);
    }

    public Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Milestone> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }
}
