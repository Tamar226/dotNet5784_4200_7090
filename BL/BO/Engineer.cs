namespace BO;

public class Engineer
{
    public int IdEngineer { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience level { get; set; }
    public double Cost { get; set;}
    public BO.TaskInEngineer? Task{get; set;}
   
    //public override string ToString() => this.ToStringProperty();
    

}
