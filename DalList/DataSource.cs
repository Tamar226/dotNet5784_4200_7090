namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        // Task
        internal const int Id_number_Task = 0;
        private static int Id_next_number_Task = Id_number_Task;
        internal static int ID_next_number_Task { get => Id_next_number_Task++; }
        //Milestone
        internal const int Id_number_Milestone = 0;
        private static int Id_next_number_Milestone = Id_number_Milestone;
        internal static int ID_next_number_Milestone { get => Id_next_number_Milestone++; }
    }
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Milestone> Milestones { get; } = new();

}
