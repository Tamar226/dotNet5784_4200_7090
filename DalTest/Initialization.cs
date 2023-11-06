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




    }

    /// <summary>
    /// creat new Dependence. a loop that create each one of the dependences own.
    /// </summary>
    private static void createDependence()
    {


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
