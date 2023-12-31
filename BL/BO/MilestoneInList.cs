namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }
    public string? description { get; set; }
    public string? Alias { get; set; }
    public BO.status status { get; set; }
    public double? CompletionPercentage { get; set; }
}
