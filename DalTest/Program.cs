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
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static IDependence? s_dalDependence = new DependenceImplementation();//stage 1
    static readonly IDal s_dal = new DalList(); //stage 2
 
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dal); //stage 2 //create the data-base by Initialization 
            chooseEntities();
        }
        catch (DalAlreadyExistsException ex)//catch the exeption from type "DalAlreadyExistsException" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (DalDeletionImpossible ex)//catch the exeption from type "DalDeletionImpossible" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (DalErrorINput ex)//catch the exeption from type "DalErrorINput" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (DalDoesNotExistException ex)//catch the exeption from type "DalDoesNotExistException" who were until here...
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
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_dal!.Task.Create(createTask());
                     break;
            case 2:
                printTask(s_dal!.Task.Read(idToRead()));
                break;

            case 3:
                List<DO.Task> listOfTask = s_dal!.Task.ReadAll();
                foreach (DO.Task t in listOfTask)
                {
                    printTask(t);
                   
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_dal!.Task.Update( UpdateMyTask(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_dal!.Task.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// The createTask function prompts for task details and returns a Task object.
    /// </summary>
    public static DO.Task createTask(int idToUpdate=0)
    {
        Console.WriteLine("Enter Description:");
        string? description = Console.ReadLine();
        if(!(description is string))
        {
            throw new DalErrorINput(" You suppose to input a string"); 
        }
        Console.WriteLine("Enter Nickname:");
        string? nickname = Console.ReadLine();
        if (!(nickname is string))
        {
            throw new DalErrorINput(" You suppose to input a string");
        }
        Console.WriteLine("Enter Milestone (true/false):");
        bool milestone = bool.Parse(Console.ReadLine());
        if (!(milestone is bool))
        {
            throw new DalErrorINput(" You suppose to input a milestone");
        }
        Console.WriteLine("Enter Product:");
        string? product = Console.ReadLine();
        if (!(product is string))
        {
            throw new DalErrorINput(" You suppose to input a string");
        }
        Console.WriteLine("Enter Notes:");
        string? notes = Console.ReadLine();
        if (!(notes is string))
        {
            throw new DalErrorINput(" You suppose to input a string");
        }
        Difficulty difficulty;
        Console.WriteLine("Enter the task's level: ( Novice, AdvancedBeginner, Competent, Proficient, Expert)");
        Difficulty.TryParse(Console.ReadLine(), out difficulty);
        if (!(difficulty is Difficulty))
        {
            throw new DalErrorINput(" You suppose to input a level for the task");
        }
        Console.WriteLine("Enter idEngineer:");
        int _idEngineer = int.Parse(Console.ReadLine());
        if (!(_idEngineer is int))
        {
            throw new DalErrorINput(" You suppose to input a number");
        }
        Console.WriteLine("Enter Creation Date (optional):");
        DateTime? creationDate = null;
        string? creationDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(creationDateString))
        {
            creationDate = DateTime.Parse(creationDateString);
        }
        if (!(creationDate is DateTime))
        {
            throw new DalErrorINput(" You suppose to input a date");
        }
        Console.WriteLine("Enter Start Date (optional):");
        DateTime? startDate = null;
        string? startDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(startDateString))
        {
            startDate = DateTime.Parse(startDateString);
        }
        if (!(startDate is DateTime))
        {
            throw new DalErrorINput(" You suppose to input a date");
        }
        Console.WriteLine("Enter Forecast Date (optional):");
        DateTime? forecastDate = null;
        string? forecastDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(forecastDateString))
        {
            forecastDate = DateTime.Parse(forecastDateString);
        }
        if (!(forecastDate is DateTime))
        {
            throw new DalErrorINput(" You suppose to input a date");
        }
        Console.WriteLine("Enter LastE (optional):");
        DateTime? lastE = null;
        string? lastEString = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastEString))
        {
            lastE = DateTime.Parse(lastEString);
        }
        if (!(lastE is DateTime))
        {
            throw new DalErrorINput(" You suppose to input a date");
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
        Console.WriteLine("Actual EndDate: " + task.ActualEndDate+"\n");
        
    }
    public static DO.Task UpdateMyTask(int id)
    {
        //s_dalTask!.Update(createTask(id));
        DO.Task myTask = s_dal!.Task.Read(id);
        printTask(myTask);
        Console.WriteLine("Please enter what do you want to update in your task:");
        string? inputToUpdate = null;
        //Request new input
        Console.WriteLine("Enter new description:");
        inputToUpdate = Console.ReadLine();
        string description = string.IsNullOrEmpty(inputToUpdate) ? myTask.Description : inputToUpdate;

        Console.WriteLine("Enter new nickname:");
        inputToUpdate = Console.ReadLine();
        string nickname = string.IsNullOrEmpty(inputToUpdate) ? myTask.Nickname : inputToUpdate;

        Console.WriteLine("Enter new milestone status:");
        inputToUpdate = Console.ReadLine();
        bool milestone = string.IsNullOrEmpty(inputToUpdate) ? myTask.Milestone :Convert.ToBoolean(inputToUpdate);

        Console.WriteLine("Enter new product description:");
        inputToUpdate = Console.ReadLine();
        string product = string.IsNullOrEmpty(inputToUpdate) ? myTask.Product : inputToUpdate;

        Console.WriteLine("Enter new notes:");
        inputToUpdate = Console.ReadLine();
        string notes = string.IsNullOrEmpty(inputToUpdate) ? myTask.Notes : inputToUpdate;
        Difficulty difficulty;
        Console.WriteLine("Enter new difficulty level:");
        inputToUpdate = Console.ReadLine();
        Difficulty.TryParse(inputToUpdate, out difficulty);

        Console.WriteLine("Enter new engineer ID:");
        inputToUpdate = Console.ReadLine();
        int _idEngineer = string.IsNullOrEmpty(inputToUpdate) ? myTask.idEngineer :Convert.ToInt32(inputToUpdate);

        Console.WriteLine("Enter new creation date:");
        inputToUpdate = Console.ReadLine();
        DateTime? creationDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.CreationDate :Convert.ToDateTime( inputToUpdate);

        Console.WriteLine("Enter new start date:");
        inputToUpdate = Console.ReadLine();
        DateTime? startDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.StartDate: Convert.ToDateTime(inputToUpdate);

        Console.WriteLine("Enter new forecast date:");
        inputToUpdate = Console.ReadLine();
        DateTime? forecastDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.foresastdate : Convert.ToDateTime(inputToUpdate);

        Console.WriteLine("Enter new last end date:");
        inputToUpdate = Console.ReadLine();
        DateTime? lastE = string.IsNullOrEmpty(inputToUpdate) ? myTask.LastEndDate : Convert.ToDateTime(inputToUpdate);

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
        return (new DO.Task(id, description, nickname, milestone, product, notes, difficulty, _idEngineer, creationDate, startDate, forecastDate, lastE, null));
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
                s_dal!.Engineer.Create(createEngineer());
                break;
            case 2:
                printEngineer(s_dal!.Engineer.Read(idToRead()));
                break;

            case 3:
                List<Engineer> listOfEngineers = s_dal!.Engineer.ReadAll();
                foreach (Engineer e in listOfEngineers)
                {
                    printEngineer(e);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Engineer to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_dal!.Engineer.Update(UpdateMyEngineer(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of engineer to delete:");
                s_dal!.Engineer.Delete(int.Parse(Console.ReadLine()));
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
        if (!(idEngineer is int)&&(idEngineer<100000000|| idEngineer>999999999))
        {
            throw new DalErrorINput(" You suppose to input a number with 9 digits");
        }
        Console.WriteLine("Enter name:");
        string nameEngineer = Console.ReadLine();
        if (!(nameEngineer is string))
        {
            throw new DalErrorINput(" You suppose to input a string");
        }
        Console.WriteLine("Enter email of engineer:");
        string emailEngineer = Console.ReadLine();
        if (!(emailEngineer is string))
        {
            throw new DalErrorINput(" You suppose to input a string");
        }

        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        Difficulty difficulty;
        string inputToUpdate = Console.ReadLine();
        Difficulty.TryParse(inputToUpdate, out difficulty);
        if (!(inputToUpdate is Difficulty))
        {
            throw new DalErrorINput(" You suppose to input a level for the engineer");
        }
        Console.WriteLine("Enter cost per hour og engineer:");
        double costPerHour = double.Parse(Console.ReadLine());
        if (!(costPerHour is double))
        {
            throw new DalErrorINput(" You suppose to input a number");
        }
        return (new Engineer(idEngineer, nameEngineer, emailEngineer, difficulty, costPerHour));
    }
    public static void printEngineer(Engineer engineer)
    {
        Console.WriteLine("The Engineer");
        Console.WriteLine("Id: " + engineer.IdNumberEngineer);
        Console.WriteLine("description: " + engineer.Name);
        Console.WriteLine("nickname: " + engineer.Email);
        Console.WriteLine("level: " + engineer.Level);
        Console.WriteLine("cost: " + engineer.Cost+"\n");  
    }
    public static DO.Engineer UpdateMyEngineer(int id)
    {
        //s_dalEngineer!.Update(createEngineer(id));
        DO.Engineer myEngineer = s_dal!.Engineer.Read(id);
        Console.WriteLine("Please enter what do you want to update in your task:");
        printEngineer(myEngineer);
        //Request new input
        Console.WriteLine("Enter name:");
        string nameEngineer = Console.ReadLine();

        Console.WriteLine("Enter email of engineer:");
        string emailEngineer = Console.ReadLine();

        Difficulty difficulty;
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        string inputLevel = Console.ReadLine();

        Console.WriteLine("Enter cost per hour og engineer:");
        string inputToUpdate = Console.ReadLine();
        double costPerHour = string.IsNullOrEmpty(inputToUpdate) ? myEngineer.Cost : Convert.ToInt32(inputToUpdate);
        //Check if input is empty
        if (string.IsNullOrEmpty(nameEngineer))
        {
            nameEngineer = myEngineer.Name;
        }
        if (string.IsNullOrEmpty(emailEngineer))
        {
            emailEngineer = myEngineer.Email;
        }
        if (string.IsNullOrEmpty(inputLevel))
        {
            difficulty = myEngineer.Level;
        }
        else
        {
            Difficulty.TryParse(inputLevel, out difficulty);
        }
      

       return (new Engineer(id, nameEngineer, emailEngineer, difficulty, costPerHour));

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

                s_dal!.Dependence.Create(createDependence());
                break;
            case 2:
                printDependence(s_dal!.Dependence.Read(idToRead()));
                break;
            case 3:
                List<Dependence> listOfDependence = s_dal!.Dependence.ReadAll();
                foreach (Dependence d in listOfDependence)
                {
                    printDependence(d);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Dependent Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_dal!.Dependence.Update( UpdateMyDependency(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_dal!.Dependence.Delete(int.Parse(Console.ReadLine()));
                break;
            default:
                break;
        }
    }
    public static DO.Dependence createDependence(int idDependence = 0)
    {
 
        Console.WriteLine("Enter Dependent Task:");
        int idDependenceTask = int.Parse(Console.ReadLine());
        if (!(idDependenceTask is int))
        {
            throw new DalErrorINput(" You suppose to input a number");
        }
        Console.WriteLine("Enter Depends On Task:");
        int DependsOnTask = int.Parse(Console.ReadLine());
        if (!(DependsOnTask is int))
        {
            throw new DalErrorINput(" You suppose to input a number");
        }
        return (new Dependence(0, idDependenceTask, DependsOnTask));
    }
    public static void printDependence(Dependence dependency)
    {
        Console.WriteLine("The Task Dependence");
        Console.WriteLine("Id: " + dependency.IdNumberDependence);
        Console.WriteLine("Dependent Task: " + dependency.DependentTask);
        Console.WriteLine("Dependent On Task: " + dependency.DependsOnTask + "\n");
    }
    public static DO.Dependence UpdateMyDependency(int id)
    {
        //s_dalDependence!.Update(createDependence(id));
        DO.Dependence myDependency = s_dal!.Dependence.Read(id);
        printDependence(myDependency);
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
        return (new Dependence(id, idDependenceTask, DependsOnTask));
    }
}