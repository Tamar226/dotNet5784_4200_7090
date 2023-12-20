using System.Reflection.Emit;

namespace DO;
public record Task
(
    int IdNumberTask,
    string? Description,
    TimeSpan? RequiredEffortTime,
    string? Alias,//כינוי
    bool Milestone,
    string? Product,//תאור התוצר
    string? Notes,//הערות
    Difficulty Level,
    int idEngineer,
    DateTime? CreatedAtDate = null,//תאריך יצירה
    DateTime? StartDate = null,//תאריך התחלה
    DateTime? foresastdate = null,//תאריך משוער לסיום
    DateTime? LastEndDate = null,//תאריך אחרון לסיום
    DateTime? ActualEndDate = null//תאריך סיום בפועל
)
{ public Task() : this(0, "",null, "", false, "", "", 0,0, null, null, null, null, null) { } } //default ctor 




