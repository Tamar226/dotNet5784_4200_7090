using BO;

namespace BlApi;

public interface IMilestone
{
    public void CreateProjectSchedule(List<DO.Task> tasks, List<DO.Dependence> dependencies);
    public BO.Milestone Read(int idMilestone);
    public void Update(BO.Milestone milestone);
}
