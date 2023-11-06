namespace DO;

public record Dependence
(
    int IdNumberDependence,
    int DependentTask,
    int DependsOnTask
)
{ public Dependence() : this(0, 0, 0) { } };
