namespace DO;
public record Task
{
    int Id_number_Task;
    string Description;
    string Nickname;
    bool Milestone;
    DateTime? Creation_Date=null;//תאריך יצירה
    DateTime? Start_Date=null;//תאריך התחלה
    DateTime? Estimated_completion_date=null;//תאריך משוער לסיום
    DateTime? Last_end_date=null;//תאריך אחרון לסיום
    DateTime? Actual_end_date=null;//תאריך סיום בפועל
    string Product;//תאור התוצר
    string Notes;//הערות
    int Engineer.Id_number_Engineer Engineer_num;
    int Level;
}

