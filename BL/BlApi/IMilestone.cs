using BO;

namespace BlApi;

public interface IMilestone
{
    public void CreateProjectSchedule();
    public BO.Milestone Read(int idMilestone);
    public void Update(BO.Milestone milestone);
}
