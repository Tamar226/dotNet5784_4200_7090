﻿namespace BO;

public class Milestone
{
    public int IDMilestone { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public BO.status Status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<BO.TaskInList>? Dependencies { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
