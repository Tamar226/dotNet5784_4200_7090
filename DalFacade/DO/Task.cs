using System.Reflection.Emit;

namespace DO;
public record Task
(
    int IdNumberTask,
    string Alias,//כינוי
    string Description,
    DateTime CreatedAtDate ,//תאריך יצירה
    TimeSpan? RequiredEffortTime,
    bool Milestone,
    string? Product,//תאור התוצר
    string? Notes,//הערות
    Difficulty Level,
    int? idEngineer,
    DateTime? StartDate = null,//תאריך התחלה
    DateTime? scheduleDate = null,//תאריך משוער לסיום
    DateTime? LastEndDate = null,//תאריך אחרון לסיום
    DateTime? ActualEndDate = null//תאריך סיום בפועל
)
{ public Task() : this(0, "", "",DateTime.Now, TimeSpan.Zero, false, "", "",Difficulty.Novice,0, null, null, null, null) { } } //default ctor 




