using BO;

namespace BlApi;

public interface IMilestone
{
    public Milestone CreateProjectSchedule(List<DO.Task> tasks, List<DO.Dependence> dependencies);
    public BO.Milestone Read(int idMilestone);
    public Milestone Update(BO.Milestone milestone);

}
