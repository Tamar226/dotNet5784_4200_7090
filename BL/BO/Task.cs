namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? description { get; set; }
    public string? Alias { get; init; }
    public DateTime createdAtDate { get; init; }
    public BO.status status { get; init; }
    public BO.MilestoneInTask milestone { get; set; }
    public DateTime? BaselineStartDate { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? SchedualStartDate { get; set; }
    public DateTime? ForecastDate { get; init; }
    public DateTime? DeadlineDate { get; init; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public BO.EngineerInTask Engineer { get; init; }
    public EngineerExperience copmlexityLevel{ get; set; }

    //public override string ToString() => this.ToStringProperty();
}

