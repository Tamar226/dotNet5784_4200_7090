﻿using DalApi;
using System.Diagnostics;

namespace Dal;

//stage 3
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public DateTime? StartDateToProject { get; set; }
    public DateTime? EndDateToProject { get; set; }
    /// <summary>
    /// Reference to the XMLTOOLS file to delete all the lists in the databases
    /// </summary>
    public void Reset()
    {
        XMLTools.ResetFile("engineers");
        XMLTools.ResetFile("tasks");
        XMLTools.ResetFile("Dependences");
    }
}
