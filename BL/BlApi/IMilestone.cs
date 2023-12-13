using BO;

namespace BlApi;

public interface IMilestone
{
    public Milestone CreateSchedule(Milestone Milestone);
    public Milestone Create(int IDMilestone);
    public Milestone Update(int IDMilestone);

}
