using System.Reflection.Emit;

namespace DO;
public record Task
(
    int IdNumberTask,
    string Description,
    string Nickname,//כינוי
    bool Milestone,
    string Product,//תאור התוצר
    string Notes,//הערות
    Difficulty Level,
    DateTime? Creation_Date = null,//תאריך יצירה
    DateTime? Start_Date = null,//תאריך התחלה
    DateTime? EstimatedCompletionDate = null,//תאריך משוער לסיום
    DateTime? LastEndDate = null,//תאריך אחרון לסיום
    DateTime? ActualEndDate = null//תאריך סיום בפועל

)
{ public Task() : this(0, "", "", false, "", "", 0, null, null, null, null, null) { } } //default ctor 




