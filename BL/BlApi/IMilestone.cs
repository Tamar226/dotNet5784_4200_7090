using BO;

namespace BlApi;

public interface IMilestone
{
    public void CreateProjectSchedule(DateTime? startDate, DateTime? endDate);
    public BO.Milestone Read(int idMilestone);
    public void Update(BO.Milestone milestone);
}
