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
    private static ITask? s_daITask;
    private static IEngineer? s_dalEngineer;
    private static IDependence? s_daIdependence;

    private static readonly Random s_rand = new();
    /// <summary>
    /// creat new task. a loop that create each one of the tasks own.
    /// </summary>
    private static void createTask()
    {
        string[] TasksNames = {"Take a shower","Wash the dishes","go to walking","aet supper","doing homework","cry","be happy","be angry",""};
        foreach (string _name in TasksNames)
        {
            int _id;
            do
                _id = s_rand.Next(0, 20);
            while (s_daITask!.Read(_id) != null);
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));

            string? _alias = (_id % 2) == 0 ? _name + "ALIAS" : null;

           // Student newStu = new(_id, _description, __nickname, _Product, _Notes, _ _name, _alias, false, _bdt);

           // s_dalStudent!.Create(newStu);
        }
    }
    /// <summary>
    /// creat new Dependence. a loop that create each one of the dependences own.
    /// </summary>
    private static void createDependence()
    {
        //IDependence help = s_daIdependence;
        List<Task> _taskList = s_daITask!.ReadAll();

        for (int i = 1; i < 40; i++)
        {
            int num1 = s_rand.Next(1, 20);


           // Dependence newDpt = new(s_daIdependence., _taskList[num1].IdNumberTask, _taskList[i - 1].IdNumberTask);
        }
       // s_dalEngineer!.Create(newDpt);
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
};
