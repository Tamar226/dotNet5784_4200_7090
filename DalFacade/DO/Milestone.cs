namespace DO;

public record Milestone
{
    int Id_number_Milestone=0;
    int Id_number_pending_task;
    int Id_number_previous_task;
    public Milestone() { } //empty ctor 
    public Milestone(int Id_number_Milestone, int Id_number_pending_task, int Id_number_previous_task) { }
}
