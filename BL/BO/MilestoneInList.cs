namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }
    public string? description { get; set; }
    public string? Alias { get; set; }
    public BO.Status status { get; set; }
    public double? CompletionPercentage { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
