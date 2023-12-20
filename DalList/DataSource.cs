namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        // Task
        internal const int IdNumberTask = 1000;
        private static int IdNextNumberTask = IdNumberTask;
        internal static int IDNextNumberTask { get => IdNextNumberTask++;}
        //Dependence
        internal const int IdNumberDependence = 1000;
        private static int IdNextNumberDependence = IdNumberDependence;
        internal static int IDNextNumberDependence { get => IdNextNumberDependence++;}
    }

    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependence> Dependences { get; } = new();

}
