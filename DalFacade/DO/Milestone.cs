namespace DO;

public record Milestone
(
    int IdNumberMilestone=0,
    int IdNumberPending_task=0,
    int IdNumberPrevious_task = 0
);

    public Milestone() { } //empty ctor 
    public Milestone(int Id_number_Milestone, int Id_number_pending_task, int Id_number_previous_task) { }

