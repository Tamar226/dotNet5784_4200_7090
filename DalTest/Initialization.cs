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
    //private static ITask? s_dalTask;//stage1
    //private static IEngineer? s_dalEngineer;//stage1
    //private static IDependence? s_dalDependence;//stage1

    private static IDal? s_dal; //stage 2

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
            string _AliasOfTask= TasksNames[s_rand.Next(0, 19)];
            
            string _ProductTask= TasksNames[s_rand.Next(0, 19)];
            
            string _NotesTask = notes[s_rand.Next(0,5)];
            
            Difficulty _level = (Difficulty)s_rand.Next(Enum.GetValues(typeof(Difficulty)).Length);
            TimeSpan _RequiredEffortTime= TimeSpan.FromDays(5);
            DateTime startDate = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime _createDateTask = startDate.AddDays(s_rand.Next(range));
            DateTime _startDateTask = DateTime.Today.AddDays(s_rand.Next(10, 300));
            DateTime? _scheduleDateTask = null;
            DateTime? _LastEndDate = null;

            Task newTask = new(0, _AliasOfTask, _nameOfTask, _createDateTask, _RequiredEffortTime, false, _ProductTask, _NotesTask,_level,0, _startDateTask, _scheduleDateTask, _LastEndDate, null);

            s_dal!.Task.Create(newTask);
        }
    }
    /// <summary>
    /// create new Dependence. A loop that create each one of the dependences own.
    /// </summary>
    private static void createDependence()
    {
        IEnumerable<Task?> _taskList = s_dal!.Task.ReadAll(); 
        for (int i = 0; i< 20; i++) 
             s_dal!.Dependence.Create(new Dependence(0, _taskList.ElementAt((i * 2) % 20)!.IdNumberTask, _taskList.ElementAt((i * 2 + s_rand.Next(0,10)) % 20)!.IdNumberTask));
      
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
                _id = s_rand.Next(200000000, 400000000);

            while (s_dal!.Engineer.Read(_id) != null);
            string _emailOfEngineer = _name.Replace(" ", "") + EngineersEmails[s_rand.Next(EngineersEmails.Length)];
            string _nameOfEngineer = _name;
            Difficulty _level = (Difficulty)s_rand.Next(Enum.GetValues(typeof(Difficulty)).Length);
            double _costPerHour = s_rand.Next(150, 312);
            Engineer newEng = new(_id, _nameOfEngineer, _emailOfEngineer, _level, _costPerHour);
            s_dal!.Engineer.Create(newEng);
        }
    }
    //public static void Do(IDal dal)//stage2
    public static void Do() //stage 4
    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");//stage 1
        //s_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");//stage 1
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");//stage 1
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal =Factory.Get; //stage 4
        createEngineer();
        createTask();
        //createDependence();
        return;
    }
}



