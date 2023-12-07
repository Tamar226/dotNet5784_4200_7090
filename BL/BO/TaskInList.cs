namespace BO;

internal class TaskInList
{
    public int Id { get; init; }
    public string? description { get; set; }
    public string? Alias { get; set; }
    public BO.status status { get; set; }

    //public override string ToString() => this.ToStringProperty();
}
