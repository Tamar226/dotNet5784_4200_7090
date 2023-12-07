namespace BO;

internal class Milestone
{
    public int Id { get; init; }
    public string? description { get; set; }
    public string? Alias { get; set; }
    public DateTime createdAtDate { get; set; }
    public BO.status status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<BO.TaskInList>? dependencies { get; set; }

    //public override string ToString() => this.ToStringProperty();
}
