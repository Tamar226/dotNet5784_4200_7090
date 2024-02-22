namespace BO;

public class TaskInList
{
    public TaskInList(int idNumberTask, string description, string alias, Status status)
    {
        IdNumberTask = idNumberTask;
        Description = description;
        Alias = alias;
        Status = status;
    }
    public override string ToString() => Tools.ToStringProperty(this);

    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public BO.Status Status { get; set; }
    public int IdNumberTask { get; }
}
