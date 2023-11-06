namespace DO;
public record Engineer
(
    int IdNumberEngineer,
    string Name,
    string Email,
    Difficulty Level,
    double Cost
)
{ public Engineer() : this(0, "", "", 0, 0.0) { } }

