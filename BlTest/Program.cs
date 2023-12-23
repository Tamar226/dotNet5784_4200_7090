﻿using DalApi;
using DO;

namespace BlTest;
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y") //stage 3
                //Initialization.Do(s_dal); //stage 2 //create the data-base by Initialization
                DalTest.Initialization.Do(); //stage 4 
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
                s_bl!.Task.Create(createTask());
                break;
            case 2:
                printTask(s_bl!.Task.Read(idToRead()));
                break;

            case 3:
                IEnumerable<DO.Task?> listOfTask = s_bl!.Task.ReadAll();
                foreach (DO.Task? t in listOfTask)
                {
                    printTask(t);

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
    public static DO.Task createTask(int idToUpdate = 0)
    {
        Console.WriteLine("Enter Description:");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter Alias:");
        string? nickname = Console.ReadLine();
        Console.WriteLine("Enter Milestone (true/false):");
        bool milestone = false;
        try
        {
            milestone = bool.Parse(Console.ReadLine()!);
        }
        catch
        {
            throw new DalErrorINput(" You suppose to input a true or false");
        }//Input integrity check
        Console.WriteLine("Enter Product:");
        string? product = Console.ReadLine();
        Console.WriteLine("Enter Notes:");
        string? notes = Console.ReadLine();
        Console.WriteLine("Enter the task's level: ( Novice, AdvancedBeginner, Competent, Proficient, Expert)");
        string? difficultyStr = Console.ReadLine();
        if (!(difficultyStr == "Novice") && !(difficultyStr == "AdvancedBeginner") && !(difficultyStr == "Competent") && !(difficultyStr == "Proficient") && !(difficultyStr == "Expert"))
        {
            throw new DalErrorINput(" You suppose to input a level for the task");
        }//Input integrity check
        Difficulty difficulty;
        Difficulty.TryParse(difficultyStr, out difficulty);
        Console.WriteLine("Enter idEngineer:");
        int _idEngineer = 0;
        try
        {
            _idEngineer = int.Parse(Console.ReadLine()!);
        }
        catch
        {
            throw new DalErrorINput(" You suppose to input a number");
        }//Input integrity check
        Console.WriteLine("Enter Creation Date (optional):");
        DateTime? creationDate = null;
        string? creationDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(creationDateString))
        {
            try
            {
                creationDate = DateTime.Parse(creationDateString);
            }
            catch
            {
                throw new DalErrorINput(" You suppose to input a date");
            }
        }//Input integrity check
        Console.WriteLine("Enter Start Date (optional):");
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
                throw new DalErrorINput(" You suppose to input a date");
            }
        }//Input integrity check
        Console.WriteLine("Enter Forecast Date (optional):");
        DateTime? forecastDate = null;
        string? forecastDateString = Console.ReadLine();
        if (!string.IsNullOrEmpty(forecastDateString))
        {
            try
            {
                forecastDate = DateTime.Parse(forecastDateString);
            }
            catch
            {
                throw new DalErrorINput(" You suppose to input a date");
            }
        }//Input integrity check
        Console.WriteLine("Enter LastE (optional):");
        DateTime? lastE = null;
        string? lastEString = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastEString))
        {
            try
            {
                lastE = DateTime.Parse(lastEString);
            }
            catch
            {
                throw new DalErrorINput(" You suppose to input a date");
            }
        }//Input integrity check
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
        try
        {
            Console.WriteLine("Id: " + task.IdNumberTask);
        }
        catch
        {
            throw new DalDoesNotExistException("An object of type Task with such an ID does not exist");
        }
        Console.WriteLine("description: " + task.Description);
        Console.WriteLine("nickname: " + task.Alias);
        Console.WriteLine("milestone: " + task.Milestone);
        Console.WriteLine("product: " + task.Product);
        Console.WriteLine("notes: " + task.Notes);
        Console.WriteLine("difficulty: " + task.Level);
        Console.WriteLine("idEngineer: " + task.idEngineer);
        Console.WriteLine("creation Date: " + task.CreatedAtDate);
        Console.WriteLine("start Date: " + task.StartDate);
        Console.WriteLine("forecast Date: " + task.scheduleDate);
        Console.WriteLine("last End Date: " + task.LastEndDate);
        Console.WriteLine("Actual EndDate: " + task.ActualEndDate + "\n");

    }
    public static DO.Task UpdateMyTask(int id)
    {
        //s_dalTask!.Update(createTask(id));
        DO.Task myTask = s_bl!.Task.Read(id);
        printTask(myTask);
        Console.WriteLine("Please enter what do you want to update in your task:");
        string? inputToUpdate = null;
        //Request new input
        Console.WriteLine("Enter new description:");
        inputToUpdate = Console.ReadLine();
        string? description = string.IsNullOrEmpty(inputToUpdate) ? myTask.Description : inputToUpdate;

        Console.WriteLine("Enter new nickname:");
        inputToUpdate = Console.ReadLine();
        string? nickname = string.IsNullOrEmpty(inputToUpdate) ? myTask.Alias : inputToUpdate;

        Console.WriteLine("Enter new milestone status:");
        inputToUpdate = Console.ReadLine();
        bool milestone = string.IsNullOrEmpty(inputToUpdate) ? myTask.Milestone : Convert.ToBoolean(inputToUpdate);

        Console.WriteLine("Enter new product description:");
        inputToUpdate = Console.ReadLine();
        string? product = string.IsNullOrEmpty(inputToUpdate) ? myTask.Product : inputToUpdate;

        Console.WriteLine("Enter new notes:");
        inputToUpdate = Console.ReadLine();
        string? notes = string.IsNullOrEmpty(inputToUpdate) ? myTask.Notes : inputToUpdate;
        Difficulty difficulty;
        Console.WriteLine("Enter new difficulty level:");
        inputToUpdate = Console.ReadLine();
        Difficulty.TryParse(inputToUpdate, out difficulty);

        Console.WriteLine("Enter new engineer ID:");
        inputToUpdate = Console.ReadLine();
        int _idEngineer = string.IsNullOrEmpty(inputToUpdate) ? myTask.idEngineer : Convert.ToInt32(inputToUpdate);

        Console.WriteLine("Enter new creation date:");
        inputToUpdate = Console.ReadLine();
        DateTime? creationDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.CreatedAtDate : Convert.ToDateTime(inputToUpdate);

        Console.WriteLine("Enter new start date:");
        inputToUpdate = Console.ReadLine();
        DateTime? startDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.StartDate : Convert.ToDateTime(inputToUpdate);

        Console.WriteLine("Enter new forecast date:");
        inputToUpdate = Console.ReadLine();
        DateTime? forecastDate = string.IsNullOrEmpty(inputToUpdate) ? myTask.scheduleDate : Convert.ToDateTime(inputToUpdate);

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
            nickname = myTask.Alias;
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
                s_bl!.Engineer.Create(createEngineer());
                break;
            case 2:
                printEngineer(s_bl!.Engineer.Read(idToRead()));
                break;

            case 3:
                IEnumerable<Engineer> listOfEngineers = s_bl!.Engineer.ReadAll();
                foreach (Engineer e in listOfEngineers)
                {
                    printEngineer(e);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Engineer to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_bl!.Engineer.Update(UpdateMyEngineer(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of engineer to delete:");
                s_bl!.Engineer.Delete(int.Parse(Console.ReadLine()));
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
            try
            {
                idEngineer = int.Parse(Console.ReadLine());
                if ((idEngineer < 100000000 || idEngineer > 999999999))
                {
                    throw new DalErrorINput(" You suppose to input a number with 9 digits");
                }
            }
            catch
            {
                throw new DalErrorINput(" You suppose to input a number with 9 digits");
            }
        }//Checking if it is automatic creation or manual data entry

        Console.WriteLine("Enter name:");
        string? nameEngineer = Console.ReadLine();
        Console.WriteLine("Enter email of engineer:");
        string? emailEngineer = Console.ReadLine();
        Console.WriteLine("Enter the engineer's level:( Novice, AdvancedBeginner, Competent, Proficient, Expert");
        Difficulty difficulty;
        string? inputToUpdate = Console.ReadLine();
        Difficulty.TryParse(inputToUpdate, out difficulty);
        if (!(inputToUpdate == "Novice") && !(inputToUpdate == "AdvancedBeginner") && !(inputToUpdate == "Competent") && !(inputToUpdate == "Proficient") && !(inputToUpdate == "Expert"))
        {
            throw new DalErrorINput(" You suppose to input a level for the engineer");
        }
        Console.WriteLine("Enter cost per hour og engineer:");
        double costPerHour = 0;
        try
        {
            double.Parse(Console.ReadLine());
            if (!(costPerHour is double))
            {
                throw new DalErrorINput(" You suppose to input a number");
            }//Input integrity check
        }
        catch
        {
            throw new DalErrorINput(" You suppose to input a number");

        }


        return (new Engineer(idEngineer, nameEngineer, emailEngineer, difficulty, costPerHour));
    }
    public static void printEngineer(Engineer engineer)
    {
        Console.WriteLine("The Engineer");
        try
        {
            Console.WriteLine("Id: " + engineer.IdNumberEngineer);
        }

        catch
        {
            throw new DalDoesNotExistException("An object of type Engineer with such an ID does not exist");
        }
        Console.WriteLine("description: " + engineer.Name);
        Console.WriteLine("nickname: " + engineer.Email);
        Console.WriteLine("level: " + engineer.Level);
        Console.WriteLine("cost: " + engineer.Cost + "\n");
    }
    public static DO.Engineer UpdateMyEngineer(int id)
    {
        //s_dalEngineer!.Update(createEngineer(id));
        DO.Engineer myEngineer = s_bl!.Engineer.Read(id);
        Console.WriteLine("Please enter what do you want to update in your task:");
        printEngineer(myEngineer);
        //Request new input
        Console.WriteLine("Enter name:");
        string? nameEngineer = Console.ReadLine();

        Console.WriteLine("Enter email of engineer:");
        string? emailEngineer = Console.ReadLine();

        Difficulty difficulty;
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

                s_bl!.Dependence.Create(createDependence());
                break;
            case 2:
                printDependence(s_bl!.Dependence.Read(idToRead()));
                break;
            case 3:
                IEnumerable<Dependence?> listOfDependence = s_bl!.Dependence.ReadAll();
                foreach (Dependence? d in listOfDependence)
                {
                    printDependence(d);
                }
                break;
            case 4:
                Console.WriteLine("Enter Id Number of Dependent Task to update:");
                int idToUpdate = int.Parse(Console.ReadLine());
                s_bl!.Dependence.Update(UpdateMyDependency(idToUpdate));
                break;
            case 5:
                Console.WriteLine("Enter Id Number of Task to delete:");
                s_bl!.Dependence.Delete(int.Parse(Console.ReadLine()));
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
        try
        {
            Console.WriteLine("Id: " + dependency.IdNumberDependence);
        }
        catch
        {
            throw new DalDoesNotExistException("An object of type Dependence with such an ID does not exist");
        }
        Console.WriteLine("Dependent Task: " + dependency.DependentTask);
        Console.WriteLine("Dependent On Task: " + dependency.DependsOnTask + "\n");
    }
    public static DO.Dependence UpdateMyDependency(int id)
    {
        //s_dalDependence!.Update(createDependence(id));
        DO.Dependence myDependency = s_bl!.Dependence.Read(id);
        printDependence(myDependency);
        Console.WriteLine("Please enter what do you want to update in your dependency:");

        //Request new input
        Console.WriteLine("Enter Dependent Task:");
        int idDependenceTask = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Depends On Task:");
        int DependsOnTask = int.Parse(Console.ReadLine());

        //Check if input is empty
        if (idDependenceTask == 0)
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