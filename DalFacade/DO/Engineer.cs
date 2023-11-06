namespace DO;
public record Engineer
(
    int IdNumberEngineer,
    string Name,
    string Email,
    int Level,
    float Cost
);
public Engineer(int Id_number_Engineer, string Name, string Email, int Level, float Cost) { }
public Engineer() { }

