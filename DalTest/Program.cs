namespace DalTest;
using DO;
using Dal;
using DalApi;
using System.Collections.Specialized;
using System.Collections.Generic;

internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static IDependence? s_dalDependence = new DependenceImplementation(); //stage 1
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalTask, s_dalDependence, s_dalEngineer);
            chooseEntities();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    //----------------------------------------------//
    //*********************************************//

    public static void chooseEntities()
    {
        int choiceEntity = 0;
        do
        {
            Console.WriteLine("please choose an option\n for Task press 1\n for Engineer press 2\n for Dependency press 3\n for exit press 0\n ");
            choiceEntity = (Convert.ToInt32(Console.ReadLine()));

            switch (choiceEntity)
            {
                case 1:
                    TaskEntity();
                    break;

                case 2:
                    EngineerEntity();
                    break;

                case 3:

                    DependencyEntity();
                    break;

                default:
                    break;
            }

        } while (choiceEntity != 0);
    }

    public static void TaskEntity()
    {
        int choiceAct = 0;
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for Update exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_dalTask!.Create(ceateTask());
                     break;
            case 2:
                printTask(s_dalTask!.Read(idToRead()));
                break;

            case 3:
                List<Task> listOfTask = s_dalTask!.ReadAll();
                foreach (Task t in listOfTask)
                {
                    Console.WriteLine(t.Description);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_dalTask!.Update(ceateTask(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_dalTask!.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    public static Task ceateTask(int idToUpdate=0)
    {
        Console.WriteLine("Enter Description:");
        string description = Console.ReadLine();

        Console.WriteLine("Enter Nickname:");
        string nickname = Console.ReadLine();

        Console.WriteLine("Enter Milestone (true/false):");
        bool milestone = bool.Parse(Console.ReadLine());

        Console.WriteLine("Enter Product:");
        string product = Console.ReadLine();

        Console.WriteLine("Enter Notes:");
        string notes = Console.ReadLine();

        Difficulty difficulty;
        Console.WriteLine("Enter the task's level");
        Difficulty.TryParse(Console.ReadLine(), out difficulty);

        Console.WriteLine("Enter idEngineer:");
        int _idEngineer = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Creation Date (optional):");
        DateTime? creationDate = null;
        string creationDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(creationDateString))
        {
            creationDate = DateTime.Parse(creationDateString);
        }

        Console.WriteLine("Enter Start Date (optional):");
        DateTime? startDate = null;
        string startDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(startDateString))
        {
            startDate = DateTime.Parse(startDateString);
        }

        Console.WriteLine("Enter Forecast Date (optional):");
        DateTime? forecastDate = null;
        string forecastDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(forecastDateString))
        {
            forecastDate = DateTime.Parse(forecastDateString);
        }

        Console.WriteLine("Enter LastE (optional):");
        DateTime? lastE = null;
        string lastEString = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastEString))
        {
            lastE = DateTime.Parse(lastEString);
        }

        return (new Task(idToUpdate, description, nickname, milestone, product, notes, difficulty, _idEngineer, creationDate, startDate, forecastDate, lastE, null));
    }
    public static int idToRead()
    {
        Console.WriteLine("Enter Id Number of Task to read:");
        return (int.Parse(Console.ReadLine()));
        
    }
    public static void printTask(Task task) {
        Console.WriteLine("The Task");
        Console.WriteLine("Id: "+ task.IdNumberTask);
        Console.WriteLine("description: " + task.Description);
        Console.WriteLine("nickname: " + task.Nickname);
        Console.WriteLine("milestone: " + task.Milestone);
        Console.WriteLine("product: " + task.Product);
        Console.WriteLine("notes: " + task.Notes);
        Console.WriteLine("difficulty: " + task.Level);
        Console.WriteLine("idEngineer: " + task.idEngineer);
        Console.WriteLine("creation Date: " + task.CreationDate);
        Console.WriteLine("start Date: " + task.StartDate);
        Console.WriteLine("forecast Date: " + task.foresastdate);
        Console.WriteLine("last End Date: " + task.LastEndDate);
        Console.WriteLine("Actual EndDate: " + task.ActualEndDate);
    }
}