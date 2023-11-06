namespace DO;
public record Task
(
    int IdNumberTask = 0,
    string Description = "",
    string Nickname = "",
    bool Milestone=false,
    string Product="",//תאור התוצר
    string Notes = "",//הערות
    //int Engineer.Id_number_Engineer Engineer_num;
    int Level = 0,
    DateTime? Creation_Date=null,//תאריך יצירה
    DateTime? Start_Date=null,//תאריך התחלה
    DateTime? EstimatedCompletionDate=null,//תאריך משוער לסיום
    DateTime? LastEndDate=null,//תאריך אחרון לסיום
    DateTime? ActualEndDate=null//תאריך סיום בפועל
);
    public Task(){ } //empty ctor 
    public Task(int Id_number_Task = 0, string Description = "", string Nickname = "", string Product="", bool Milestone = false, string Notes = "", int Level = 0, DateTime? Creation_Date = null, DateTime? Start_Date = null, DateTime? Estimated_completion_date = null, DateTime? Last_end_date = null, DateTime? Actual_end_date = null) {  }


