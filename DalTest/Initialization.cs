namespace DalTest;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class Initialization
{
    /// <summary>
    /// Access to existing interfaces
    /// </summary>
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependence? s_dalDependence;

    private static readonly Random s_rand = new();
    /// <summary>
    /// creat new task. a loop that create each one of the tasks own.
    /// </summary>
    private static void createTask()
    {
        string[] TasksNames = { "Write documentation", "Bug fixing",
            "Unit testing", "Requirements analysis", "Version upgrades",
            "User interface design", "Bug identification", "Writing automated tests",
            "Potential bug detection", "Performance improvement",
            "Identifying and fixing security dilemmas", "New feature development",
            "Updating external dependencies", "Maintenance of existing fixes",
            "Writing technical documentation", "Code quality testing",
            "Safety and security infrastructure", "Issue management and enhancements",
            "Creating API interfaces", "Configuration management" };
        string[] notes =
        {
            "do it soon!","Very critical","Work carefully","Not critical","You can help the head of the team","It is recommended to participate in a team meeting about the issue"
        };
        foreach (string _nameOfTask in TasksNames)
        {            
            string _NicknameOfTask= TasksNames[s_rand.Next(0, 19)];
            
            string _ProductTask= TasksNames[s_rand.Next(0, 19)];
            
            string _NotesTask = notes[s_rand.Next(0,5)];
            
            Difficulty _level = (Difficulty)s_rand.Next(Enum.GetValues(typeof(Difficulty)).Length);
     
            DateTime startDate = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime _createDateTask = startDate.AddDays(s_rand.Next(range));
            DateTime _startDateTask = DateTime.Today.AddDays(s_rand.Next(10, 300));
            DateTime _foresastDateTask = _startDateTask.AddDays(s_rand.Next(10, 300));
            DateTime _LastEndDate = _foresastDateTask.AddDays(10);

            Task newTask = new(0, _nameOfTask, _NicknameOfTask,false, _ProductTask, _NotesTask,_level,0, _createDateTask, _startDateTask, _foresastDateTask, _LastEndDate, null);

            s_dalTask!.Create(newTask);
        }
    }
<<<<<<< HEAD
=======

>>>>>>> d9960eb4c4de86fcf7f237c7d8abf046e36ab681
    /// <summary>
    /// creat new Dependence. a loop that create each one of the dependences own.
    /// </summary>
    private static void createDependence()
    {
        List<Task> _taskList = s_dalTask!.ReadAll();
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {

                if (j != i && s_rand.Next(0,10)>5)
                {
                    Dependence newDependence = new(0, _taskList[i].IdNumberTask, _taskList[j].IdNumberTask);
                    s_dalDependence!.Create(newDependence);
                }
            }
        }
    }

    /// <summary>
    /// creat new Engineer. a loop that create each one of the engineers own.
    /// </summary>
    private static void createEngineer()
    {
        string[] EngineersNames = { "Eli Amar", "Yair Cohen", "Ariela Levin", "Dina Klein", "Shira Israelof" };
        string[] EngineersEmails = { "@gmail.com", "@org.co.il", "@gov.il", "@outlook.com", "@net.com" };
        foreach (string _name in EngineersNames)
        {
            int _id;
            do
                _id = s_rand.Next(100000000, 999999999);
            while (s_dalEngineer!.Read(_id) != null);
            string _emailOfEngineer = _name.Replace(" ", "") + EngineersEmails[s_rand.Next(EngineersEmails.Length)];
            string _nameOfEngineer = _name;
            Difficulty _level = (Difficulty)s_rand.Next(Enum.GetValues(typeof(Difficulty)).Length);
            double _costPerHour = s_rand.Next(150, 312);
            Engineer newEng = new(_id, _nameOfEngineer, _emailOfEngineer, _level, _costPerHour);

            s_dalEngineer!.Create(newEng);
        }
    }
    public static void Do(ITask? dalTask, IDependence? dalDependence, IEngineer? dalEngineer)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        createTask();
        createEngineer();
        createDependence();
        return;
    }
};



