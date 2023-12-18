namespace BO;

public class Task
{
    public int IdTask { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; init; }
    public MilestoneInTask? Milestone { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public BO.status Status { get; init; }
    public DateTime? BaselineStartDate { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? SchedualStartDate { get; set; }
    public DateTime? ForecastDate { get; init; }
    public DateTime? DeadlineDate { get; init; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; init; }
    public EngineerExperience copmlexityLevel{ get; set; }

    //public override string ToString() => this.ToStringProperty();
}

