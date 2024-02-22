
namespace BO;

public class Task
{
   
    public int IdTask { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; init; }
    public MilestoneInTask? Milestone { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public BO.Status Status { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? SchedualStartDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; init; }
    public EngineerExperience CopmlexityLevel{ get; set; }
    public override string ToString() =>Tools.ToStringProperty( this);
}

