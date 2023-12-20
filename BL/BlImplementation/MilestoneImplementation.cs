using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    public Milestone Create(int IDMilestone)
    {
        throw new NotImplementedException();
    }

    public Milestone CreateSchedule()
    {
        List<DO.Task> tasks =DO.Task.readAll();
        List<DO.Dependence> dependencies = DO.Dependence.readAll();
        var listGroup=from item in dependencies
                      group item by item.DependsOnTask into newGroup
                      select newGroup;
        List<BO.TaskInList> listDependencies = new List<TaskInList>();
        foreach (var item in listGroup)
        {
            new BO.Milestone()
        }
    }

    public Milestone Update(int IDMilestone)
    {
        throw new NotImplementedException();
    }
}
