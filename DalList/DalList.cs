namespace Dal;
using DalApi;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public ITask Task =>  new TaskImplementation();
    public IEngineer Engineer =>  new EngineerImplementation();

    public IDependence Dependence =>  new DependenceImplementation();
    public DateTime? StartDateToProject { get; set; }
    public DateTime? EndDateToProject { get; set; }
}
