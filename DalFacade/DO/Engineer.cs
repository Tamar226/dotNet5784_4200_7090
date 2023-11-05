namespace DO;
public record Engineer
{
    int Id_number_Engineer;
    string Name;
    string Email;
    int Level;
    float Cost;
    Engineer(int Id_number_Engineer, string Name, string Email, int Level, float Cost) { }
    Engineer() { }
}
