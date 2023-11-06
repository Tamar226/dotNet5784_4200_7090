namespace DO;

public record Dependence
(
    int IdNumberDependence,
    int IdNumberPendingTask,
    int IdNumberPreviousTask
)
{ public Dependence() : this(0, 0, 0) { } };
