namespace DalTest;
using DO;
using Dal;
using DalApi;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// The main program. Creating the database of all entities, as well as adding entities by the user according to his personal desire.
/// </summary>
internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation(); 
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); 
    private static IDependence? s_dalDependence = new DependenceImplementation(); 
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalTask, s_dalDependence, s_dalEngineer);//create the data-base by Initialization 
            chooseEntities();
        }
        catch (Exception ex)//catch the exeptions who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //----------------------------------------------//
    /// <summary>
    /// A method that allows the user to choose which type of entity he wants to create. 
    /// (Used by helper methods later in the file...)
    /// </summary>

    public static void chooseEntities()
    {
        int choiceEntity = 0;
        do
        {
            Console.WriteLine("please choose an option\n for Task press 1\n for Engineer press 2\n for Dependency press 3\n for exit press 0\n ");
            choiceEntity = (Convert.ToInt32(Console.ReadLine()));//Convert to intiger type

            switch (choiceEntity)
            {
                case 1:
                    TaskEntity();
                    break;

                case 2:
                    EngineerEntity();
                    break;

                case 3:
                    DependenceEntity();
                    break;

                default:
                    break;
            }

        } while (choiceEntity != 0);
    }

    /// <summary>
    /// Helper functions for creating an entity of task.
    /// The user chooses which action to run on his to-do list, and makes changes accordingly. 
    /// Use of printing, and updating.
    /// </summary>
    public static void TaskEntity()
    {
        //Option to choose which interface function to run on the entity
        int choiceAct = 0;
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for Update exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_dalTask!.Create(createTask());
                     break;
            case 2:
                printTask(s_dalTask!.Read(idToRead()));
                break;

            case 3:
                List<DO.Task> listOfTask = s_dalTask!.ReadAll();
                foreach (DO.Task t in listOfTask)
                {
                    Console.WriteLine(t.Description);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                UpdateMyTask(idToUpdate);
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_dalTask!.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    public static DO.Task createTask(int idToUpdate=0)
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
        Console.WriteLine("Enter the task's level: ( Novice, AdvancedBeginner, Competent, Proficient, Expert)");
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

        return (new DO.Task(idToUpdate, description, nickname, milestone, product, notes, difficulty, _idEngineer, creationDate, startDate, forecastDate, lastE, null));
    }
    public static int idToRead()
    {
        Console.WriteLine("Enter Id Number of Task to read:");
        return (int.Parse(Console.ReadLine()));
        
    }
    public static void printTask(DO.Task task)
    { 
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
    public static DO.Task UpdateMyTask(int id)
    {
        s_dalTask!.Update(createTask(id));
        DO.Task myTask = s_dalTask!.Read(id);
        Console.WriteLine(myTask);
        Console.WriteLine("Please enter what do you want to update in your task:");
       
        //Request new input
        Console.WriteLine("Enter new description:");
        string description = Console.ReadLine();

        Console.WriteLine("Enter new nickname:");
        string nickname = Console.ReadLine();

        Console.WriteLine("Enter new milestone status:");
        bool milestone = Convert.ToBoolean(Console.ReadLine());

        Console.WriteLine("Enter new product description:");
        string product = Console.ReadLine();

        Console.WriteLine("Enter new notes:");
        string notes = Console.ReadLine();

        Difficulty difficulty;
        Console.WriteLine("Enter new difficulty level:");
        Difficulty.TryParse(Console.ReadLine(), out difficulty);

        Console.WriteLine("Enter new engineer ID:");
        int _idEngineer = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter new creation date:");
        DateTime? creationDate = null;
        string creationDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(creationDateString))
        {
            creationDate = DateTime.Parse(creationDateString);
        }

        Console.WriteLine("Enter new start date:");
        DateTime? startDate = null;
        string startDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(startDateString))
        {
            startDate = DateTime.Parse(startDateString);
        }

        Console.WriteLine("Enter new forecast date:");
        DateTime? forecastDate = null;
        string forecastDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(forecastDateString))
        {
            forecastDate = DateTime.Parse(forecastDateString);
        }

        Console.WriteLine("Enter new last end date:");
        DateTime? lastE = null;
        string lastEString = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastEString))
        {
            lastE = DateTime.Parse(lastEString);
        }

        Console.WriteLine("Enter new actual end date:");
        DateTime? lastactEndDate = null;
        string lastactual = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastactual))
        {
            lastE = DateTime.Parse(lastactual);
        }

        //Check if input is empty
        if (string.IsNullOrEmpty(description))
        {
            description = myTask.Description;
        }
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = myTask.Nickname;
        }
        if (string.IsNullOrEmpty(product))
        {
            product = myTask.Product;
        }
        if (string.IsNullOrEmpty(notes))
        {
            notes = myTask.Notes;
        }
        //create an update task
        return (new DO.Task(0, description, nickname, milestone, product, notes, difficulty, _idEngineer, creationDate, startDate, forecastDate, lastE, null));
    }
    /// <summary>
    /// Helper functions for creating an entity of type engineer. 
    /// The user chooses which action to run on his to-do list, and makes changes accordingly.
    /// Use of printing, and updating.
    /// </summary>
    public static void EngineerEntity()
    {
        //Option to choose which interface function to run on the entity
        int choiceAct = 0;
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for Update exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_dalEngineer!.Create(createEngineer());
                break;
            case 2:
                printEngineer(s_dalEngineer!.Read(idToRead()));
                break;

            case 3:
                List<Engineer> listOfEngineers = s_dalEngineer!.ReadAll();
                foreach (Engineer e in listOfEngineers)
                {
                    Console.WriteLine(e.Name);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Engineer to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                UpdateMyEngineer(idToUpdate);
                break;
            case 5:
                Console.WriteLine("Enter Id Number of engineer to delete:");
                s_dalEngineer!.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    public static DO.Engineer createEngineer(int idEngineer = 0)
    {
        if (idEngineer == 0)
        {
        Console.WriteLine("Enter id of engineer:");
        idEngineer = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Enter name:");
        string nameEngineer = Console.ReadLine();

        Console.WriteLine("Enter email of engineer:");
        string emailEngineer = Console.ReadLine();

        Difficulty difficulty;
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        Difficulty.TryParse(Console.ReadLine(), out difficulty);

        Console.WriteLine("Enter cost per hour og engineer:");
        double costPerHour = double.Parse(Console.ReadLine());

        return (new Engineer(idEngineer, nameEngineer, emailEngineer, difficulty, costPerHour));
    }
    public static void printEngineer(Engineer engineer)
    {
        Console.WriteLine("The Engineer");
        Console.WriteLine("Id: " + engineer.IdNumberEngineer);
        Console.WriteLine("description: " + engineer.Name);
        Console.WriteLine("nickname: " + engineer.Email);
        Console.WriteLine("milestone: " + engineer.Level);
        Console.WriteLine("milestone: " + engineer.Cost);  
    }
    public static DO.Engineer UpdateMyEngineer(int id)
    {
        s_dalEngineer!.Update(createEngineer(id));
        DO.Engineer myEngineer = s_dalEngineer!.Read(id);
        Console.WriteLine(myEngineer);
        Console.WriteLine("Please enter what do you want to update in your task:");

        //Request new input
        Console.WriteLine("Enter name:");
        string nameEngineer = Console.ReadLine();

        Console.WriteLine("Enter email of engineer:");
        string emailEngineer = Console.ReadLine();

        Difficulty difficulty;
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        Difficulty.TryParse(Console.ReadLine(), out difficulty);

        Console.WriteLine("Enter cost per hour og engineer:");
        double costPerHour = double.Parse(Console.ReadLine());

        //Check if input is empty
        if (string.IsNullOrEmpty(nameEngineer))
        {
            nameEngineer = myEngineer.Name;
        }
        if (string.IsNullOrEmpty(emailEngineer))
        {
            emailEngineer = myEngineer.Email;
        }
        if (costPerHour==0)
        {
            costPerHour = myEngineer.Cost;
        }

        return (new Engineer(0, nameEngineer, emailEngineer, difficulty, costPerHour));

    }
    /// <summary>
    /// Helper functions for creating an entity of type dependency.
    /// The user chooses which action to run on his to-do list, and makes changes accordingly. 
    /// Use of printing, and updating.
    /// </summary>
    public static void DependenceEntity()
    {
        //Option to choose which interface function to run on the entity
        int choiceAct = 0;
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for Update exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_dalDependence!.Create(createDependence());
                break;
            case 2:
                printDependence(s_dalDependence!.Read(idToRead()));
                break;
            case 3:
                List<Dependence> listOfDependence = s_dalDependence!.ReadAll();
                foreach (Dependence d in listOfDependence)
                {
                    Console.WriteLine(d.IdNumberDependence);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Dependent Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                UpdateMyDependency(idToUpdate);
                
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_dalDependence!.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    public static DO.Dependence createDependence(int idDependence = 0)
    {
        if (idDependence == 0)
        {
            Console.WriteLine("Enter id of dependence:");
            idDependence = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Enter Dependent Task:");
        int idDependenceTask = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Depends On Task:");
        int DependsOnTask = int.Parse(Console.ReadLine());

        return (new Dependence(idDependence, idDependenceTask, DependsOnTask));
    }
    public static void printDependence(Dependence dependency)
    {
        Console.WriteLine("The Task Dependence");
        Console.WriteLine("Id: " + dependency.IdNumberDependence);
        Console.WriteLine("Dependent Task: " + dependency.DependentTask);
        Console.WriteLine("Dependent On Task: " + dependency.DependsOnTask);
    }
    public static DO.Dependence UpdateMyDependency(int id)
    {
        s_dalDependence!.Update(createDependence(id));
        DO.Dependence myDependency = s_dalDependence!.Read(id);
        Console.WriteLine(myDependency);
        Console.WriteLine("Please enter what do you want to update in your dependency:");

        //Request new input
        Console.WriteLine("Enter Dependent Task:");
        int idDependenceTask = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Depends On Task:");
        int DependsOnTask = int.Parse(Console.ReadLine());

        //Check if input is empty
        if (idDependenceTask==0)
        {
            idDependenceTask = myDependency.DependentTask;
        }
        if (DependsOnTask == 0)
        {
            DependsOnTask = myDependency.DependsOnTask;
        }
        return (new Dependence(0, idDependenceTask, DependsOnTask));
    }
}