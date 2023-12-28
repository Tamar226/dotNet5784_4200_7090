using BO;
using DalApi;
using DO;
using System.Runtime.CompilerServices;


/// <summary>
/// The main program. Creating the database of all entities, as well as adding entities by the user according to his personal desire.
/// </summary>
internal class Program
{
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static IDependence? s_dalDependence = new DependenceImplementation();//stage 1
    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();//stage 4

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y" || ans == "y")
                DalTest.Initialization.Do();
            else
            {
                if (ans != "n" && ans != "N")
                    throw new FormatException("Wrong input");
            }
            chooseEntities();
        }
        catch (BlAlreadyExistsException ex)//catch the exeption from type "DalAlreadyExistsException" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (BlDeletionImpossible ex)//catch the exeption from type "DalDeletionImpossible" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (BlErrorINput ex)//catch the exeption from type "DalErrorINput" who were until here...
        {
            Console.WriteLine(ex.ToString());
        }
        catch (BlDoesNotExistException ex)//catch the exeption from type "DalDoesNotExistException" who were until here...
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
            Console.WriteLine("please choose an option\n for Task press 1\n for Engineer press 2\n for Milestone press 3\n for exit press 0\n ");
            choiceEntity = (Convert.ToInt32(Console.ReadLine()));//Convert to intiger type
            //DeterminingProjectTimes();
            switch (choiceEntity)
            {
                case 1:
                    TaskEntity();
                    break;

                case 2:
                    EngineerEntity();
                    break;

                case 3:
                    MilestoneEntity();
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
                s_bl!.Task.Create(createTask());
                break;
            case 2:
                printTask(s_bl!.Task.Read(idToRead()));
                break;
            case 3:
                IEnumerable<BO.Task?> listOfTask = s_bl!.Task.ReadAll();
                foreach (BO.Task? t in listOfTask)
                {
                    printTask(t!);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine()!);
                s_bl!.Task.Update(UpdateMyTask(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_bl!.Task.Delete(int.Parse(Console.ReadLine()!));
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// The createTask function prompts for task details and returns a Task object.
    /// </summary>
    public static BO.Task createTask(int idToUpdate = 0)//יש פה שגיאה
    {
        Console.WriteLine("Lets creat a new task \n Enter Description:");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter Alias:");
        string? alias = Console.ReadLine();
        Console.WriteLine("How many days you think will take you?");
        TimeSpan? taskDuring = null;
        string? taskDuringString = Console.ReadLine();
        if (!string.IsNullOrEmpty(taskDuringString))
        {
            try
            {
                taskDuring = TimeSpan.Parse(taskDuringString);
            }
            catch
            {
                throw new BlErrorINput(" You suppose to input a date");
            }
        }
        Console.WriteLine("Enter Start Date:");
        DateTime? startDate = null;
        string? startDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(startDateString))
        {
            try
            {
                startDate = DateTime.Parse(startDateString);
            }
            catch
            {
                throw new BlErrorINput(" You suppose to input a date");
            }
        }
        Console.WriteLine("Enter Deliverables:");
        string? deliverables = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? remarks = Console.ReadLine();
        Console.WriteLine("Enter the task's level: ( Novice, AdvancedBeginner, Competent, Proficient, Expert)");
        string? difficultyStr = Console.ReadLine();
        if (!(difficultyStr == "Novice") && !(difficultyStr == "AdvancedBeginner") && !(difficultyStr == "Competent") && !(difficultyStr == "Proficient") && !(difficultyStr == "Expert"))
        {
            throw new BlErrorINput(" You suppose to input a level for the task");
        }//Input integrity check
        EngineerExperience difficulty;
        EngineerExperience.TryParse(difficultyStr, out difficulty);

        return (new BO.Task { IdTask = 0, Description = description, Alias = alias, CreatedAtDate = DateTime.Now, Status = (BO.status)0, Milestone = null!, BaselineStartDate = null!, StartDate = startDate, SchedualStartDate = null!, ForecastDate = startDate + taskDuring, DeadlineDate = null!, CompleteDate = null!, Deliverables = deliverables, Remarks = remarks, Engineer = null, CopmlexityLevel = difficulty });
    }
    public static int idToRead()
    {
        Console.WriteLine("Enter Id Number of Task to read:");
        return (int.Parse(Console.ReadLine()!));
    }
    public static void printTask(BO.Task task)
    {

        Console.WriteLine("The Task");
        try
        {
            Console.WriteLine("Id: " + task.IdTask);
        }
        catch
        {
            throw new BlDoesNotExistException("An object of type Task with such an ID does not exist");
        }
        Console.WriteLine("description: " + task.Description);
        Console.WriteLine("alias: " + task.Alias);
        Console.WriteLine("creation Date: " + task.CreatedAtDate);
        Console.WriteLine("Status: " + task.Status);
        if (task.Milestone is not null) { Console.WriteLine("milestone: \n" + "Id: " + task.Milestone!.Id + "\n" + "Alias: " + task.Milestone.Alias + "\n"); };
        Console.WriteLine("start Date: " + task.BaselineStartDate);
        Console.WriteLine("start Date: " + task.StartDate);
        Console.WriteLine("Schedual Date: " + task.SchedualStartDate);
        Console.WriteLine("start Date: " + task.ForecastDate);
        Console.WriteLine("last End Date: " + task.DeadlineDate);
        Console.WriteLine("Complete Date: " + task.CompleteDate);
        Console.WriteLine("product: " + task.Deliverables);
        Console.WriteLine("Remarks: " + task.Remarks);
        if (task.Engineer is not null) { Console.WriteLine("Engineer:  \n" + task.Engineer + "Id: " + task.Engineer!.Id + "\n" + "Name: " + task.Engineer.Name + "\n"); };
        Console.WriteLine("CopmlexityLevel: " + task.CopmlexityLevel + "\n");

    }
    public static BO.Task UpdateMyTask(int id)
    {
        //s_dalTask!.Update(createTask(id));
        BO.Task? myTask = s_bl!.Task.Read(id);
        printTask(myTask!);
        Console.WriteLine("Please enter what do you want to update in your task:");
        string? inputToUpdate = null;
        //Request new input
        Console.WriteLine("Enter new description:");
        inputToUpdate = Console.ReadLine();
        string? description = string.IsNullOrEmpty(inputToUpdate) ? myTask!.Description : inputToUpdate;
        Console.WriteLine("Enter new alias:");
        inputToUpdate = Console.ReadLine();
        string? alias = string.IsNullOrEmpty(inputToUpdate) ? myTask!.Alias : inputToUpdate;
        Console.WriteLine("Enter new start date:");
        inputToUpdate = Console.ReadLine();
        DateTime? startDate = string.IsNullOrEmpty(inputToUpdate) ? myTask!.StartDate : Convert.ToDateTime(inputToUpdate);
        Console.WriteLine("Enter new product description:");
        inputToUpdate = Console.ReadLine();
        string? deliverables = string.IsNullOrEmpty(inputToUpdate) ? myTask!.Deliverables : inputToUpdate;

        Console.WriteLine("Enter new notes:");
        inputToUpdate = Console.ReadLine();
        string? remarks = string.IsNullOrEmpty(inputToUpdate) ? myTask!.Remarks : inputToUpdate;
        EngineerExperience difficulty;
        Console.WriteLine("Enter new difficulty level:");
        inputToUpdate = Console.ReadLine();
        EngineerExperience.TryParse(inputToUpdate, out difficulty);

        //Check if input is empty
        if (string.IsNullOrEmpty(description))
        {
            description = myTask!.Description;
        }
        if (string.IsNullOrEmpty(alias))
        {
            alias = myTask!.Alias;
        }
        if (string.IsNullOrEmpty(deliverables))
        {
            deliverables = myTask!.Deliverables;
        }
        if (string.IsNullOrEmpty(remarks))
        {
            remarks = myTask!.Remarks;
        }
        //create an update task
        return (new BO.Task { IdTask = id, Description = description, Alias = alias, CreatedAtDate = myTask.CreatedAtDate, Status = myTask.Status, Milestone = myTask.Milestone, BaselineStartDate = myTask.BaselineStartDate, StartDate = startDate, SchedualStartDate = myTask.SchedualStartDate, ForecastDate = myTask.ForecastDate, DeadlineDate = myTask.DeadlineDate, CompleteDate = myTask.CompleteDate, Deliverables = deliverables, Remarks = remarks, Engineer = myTask.Engineer, CopmlexityLevel = difficulty });
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
        Console.WriteLine("please choose an option\n for Create press 1\n for Read press 2\n for Read all press 3\n for Update press 4\n for Delete press 5\n for exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_bl!.Engineer.Create(createEngineer());
                break;
            case 2:
                printEngineer(s_bl!.Engineer.Read(idToRead())!);
                break;
            case 3:
                IEnumerable<BO.Engineer> listOfEngineers = s_bl!.Engineer.ReadAll()!;
                foreach (BO.Engineer e in listOfEngineers)
                {
                    printEngineer(e);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Engineer to update:");
                int idToUpdate = int.Parse(Console.ReadLine()!);
                s_bl!.Engineer.Update(UpdateMyEngineer(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of engineer to delete:");
                s_bl!.Engineer.Delete(int.Parse(Console.ReadLine()!));
                break;
            default:
                break;
        }
    }
    public static BO.Engineer createEngineer(int idEngineer = 0)
    {
        if (idEngineer == 0)
        {
            Console.WriteLine("Enter id of engineer:");
            try
            {
                idEngineer = int.Parse(Console.ReadLine()!);
                if ((idEngineer < 100000000 || idEngineer > 999999999))
                {
                    throw new BlErrorINput("You suppose to input a number with 9 digits");
                }
            }
            catch
            {
                throw new BlErrorINput("You suppose to input a number with 9 digits");
            }
        }//Checking if it is automatic creation or manual data entry

        Console.WriteLine("Enter name:");
        string? nameEngineer = Console.ReadLine();
        Console.WriteLine("Enter email of engineer:");
        string? emailEngineer = Console.ReadLine();
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        EngineerExperience difficulty;
        string? inputToUpdate = Console.ReadLine();
        EngineerExperience.TryParse(inputToUpdate, out difficulty);
        if (!(inputToUpdate == "Novice") && !(inputToUpdate == "AdvancedBeginner") && !(inputToUpdate == "Competent") && !(inputToUpdate == "Proficient") && !(inputToUpdate == "Expert"))
        {
            throw new BlErrorINput(" You suppose to input a level for the engineer");
        }
        Console.WriteLine("Enter cost per hour og engineer:");
        double costPerHour = 0;
        try
        {
            double.Parse(Console.ReadLine()!);
        }
        catch
        {
            throw new BlErrorINput(" You suppose to input a number");
        }
        return (new BO.Engineer { IdEngineer = idEngineer, Name = nameEngineer, Email = emailEngineer, Level = difficulty, Cost = costPerHour, Task = null });
    }
    public static void printEngineer(BO.Engineer engineer)
    {
        Console.WriteLine("The Engineer: ");
        try
        {
            Console.WriteLine("Id: " + engineer.IdEngineer);
        }
        catch
        {
            throw new BlDoesNotExistException("An object of type Engineer with such an ID does not exist");
        }
        Console.WriteLine("Name: " + engineer.Name);
        Console.WriteLine("Alias: " + engineer.Email);
        Console.WriteLine("Level: " + engineer.Level);
        Console.WriteLine("Cost: " + engineer.Cost);
        Console.WriteLine("Task: " + engineer.Task ?? "No Task \n");
    }
    public static BO.Engineer UpdateMyEngineer(int id)
    {
        //s_dalEngineer!.Update(createEngineer(id));
        BO.Engineer myEngineer = s_bl!.Engineer.Read(id)!;
        Console.WriteLine("Please enter what do you want to update in your task:");
        printEngineer(myEngineer);
        //Request new input
        Console.WriteLine("Enter name:");
        string? nameEngineer = Console.ReadLine();

        Console.WriteLine("Enter email of engineer:");
        string? emailEngineer = Console.ReadLine();

        EngineerExperience difficulty;
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        string? inputLevel = Console.ReadLine();

        Console.WriteLine("Enter cost per hour og engineer:");
        string? inputToUpdate = Console.ReadLine();
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
            EngineerExperience.TryParse(inputLevel, out difficulty);
        }
        return (new BO.Engineer { IdEngineer = id, Name = nameEngineer, Email = emailEngineer, Level = difficulty, Cost = costPerHour, Task = myEngineer.Task });
    }
    /// <summary>
    /// Helper functions for creating an entity of type dependency.
    /// The user chooses which action to run on his to-do list, and makes changes accordingly. 
    /// Use of printing, and updating.
    /// </summary>
    public static void MilestoneEntity()
    {
        //Option to choose which interface function to run on the entity
        int choiceAct = 0;
        Console.WriteLine("please choose an option\n for Create Project Schedule press 1\n for Read press 2\n for Update all press 3\n for Update exit 0\n");
        choiceAct = (Convert.ToInt32(Console.ReadLine()));
        switch (choiceAct)
        {
            case 1:
                s_bl!.Milestone.CreateProjectSchedule();
                break;
            case 2:
                printMilestone(s_bl!.Milestone.Read(idToRead())!);

                break;
            case 3:
                Console.WriteLine("Enter Id Number of Dependent Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine()!);
                s_bl!.Milestone.Update(UpdateMyMilestone(idToUpdate));
                break;
            default:
                break;
        }
    }
    public static void printMilestone(BO.Milestone milestone)
    {
        Console.WriteLine("The Milestone:");
        try
        {
            Console.WriteLine("Id: " + milestone.IDMilestone);
        }
        catch
        {
            throw new BlDoesNotExistException("An object of type Dependence with such an ID does not exist");
        }
        Console.WriteLine("Id: " + milestone.IDMilestone + "\n");
        Console.WriteLine("Description: " + milestone.Description + "\n");
        Console.WriteLine("Alias: " + milestone.Alias + "\n");
        Console.WriteLine("create at date: " + milestone.CreatedAtDate + "\n");
        Console.WriteLine("Status: " + milestone.Status + "\n");
        Console.WriteLine("forecast date: " + milestone.ForecastDate + "\n");
        Console.WriteLine("Deadline date: " + milestone.DeadlineDate + "\n");
        Console.WriteLine("Complete date: " + milestone.CompleteDate + "\n");
        Console.WriteLine("Completion percentage" + milestone.CompletionPercentage + "\n");
        Console.WriteLine("Remarks" + milestone.Remarks + "\n");
        Console.WriteLine("Dependencies:" + "\n");
        foreach (var oneDep in milestone.Dependencies!)
        {
            Console.WriteLine("Dep: " + "Id: " + oneDep.Id + "Alias:" + oneDep.Alias + "\n");
        }
    }
    public static BO.Milestone UpdateMyMilestone(int id)
    {
        BO.Milestone myMilestone = s_bl!.Milestone.Read(id)!;
        printMilestone(myMilestone);
        Console.WriteLine("Please enter what do you want to update in your milestone:");

        Console.WriteLine("Enter description Task:");
        string? description = Console.ReadLine()!;

        //Request new input
        Console.WriteLine("Enter alias Task:");
        string? alias = Console.ReadLine()!;

        Console.WriteLine("Enter remaks milestone:");
        string? remaks = Console.ReadLine()!;

        //Check if input is empty
        if (string.IsNullOrEmpty(description))
        {
            description = myMilestone.Description;
        }
        if (string.IsNullOrEmpty(alias))
        {
            alias = myMilestone.Alias;
        }
        if (string.IsNullOrEmpty(remaks))
        {
            remaks = myMilestone.Remarks;
        }
        return (new Milestone
        {
            IDMilestone = id,
            Description = description,
            Alias = alias,
            CreatedAtDate = myMilestone.CreatedAtDate,
            Status = myMilestone.Status,
            ForecastDate = myMilestone.ForecastDate,
            DeadlineDate = myMilestone.DeadlineDate,
            CompleteDate = myMilestone.CompleteDate,
            CompletionPercentage = myMilestone.CompletionPercentage,
            Remarks = remaks,
            Dependencies = myMilestone.Dependencies,

        });
    }
}
//private DateTime DeterminingProjectTimes()
//{
//    s_bal.EndDateToProject=;
//    s_bal.StartDateToProject;
//}