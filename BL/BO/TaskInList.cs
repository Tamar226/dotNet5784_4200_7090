namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public BO.status Status { get; set; }

    //public override string ToString() => this.ToStringProperty();
    public TaskInList(int idTask, string description, string alias, status status)
    {
        Id = idTask;
        Description = description;
        Alias = alias;
        Status = status;
    }

}
