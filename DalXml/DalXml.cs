using DalApi;
namespace Dal;

//stage 3
sealed public class DalXml : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependence => new DependenceImplementation();
}
