
namespace BO;

public class TaskInEngineer
{
    private IEnumerable<int> enumerable;
    private string v;

    public TaskInEngineer(IEnumerable<int> enumerable, string v)
    {
        this.enumerable = enumerable;
        this.v = v;
    }

    public int? Id { get; set; }
    public string? Alias { get; set; }
}
