namespace BO;

public class TaskInList
{
    public TaskInList(int idNumberTask, string description, string alias, status status)
    {
        IdNumberTask = idNumberTask;
        Description = description;
        Alias = alias;
        Status = status;
    }

    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public BO.status Status { get; set; }
    public int IdNumberTask { get; }

    //public override string ToString() => this.ToStringProperty();

}
