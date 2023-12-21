using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    public Milestone Create(int IDMilestone)
    {

    }

    public Milestone CreateProjectSchedule(List<DO.Task> tasks, List<DO.Dependence> dependencies)
    {
        DateTime dateTimeProject = DateTime.Now;
        var listOfGroupDependencies = from dep in dependencies
                                      where dep.DependentTask is not null && dep.DependsOnTask is not null
                                      group dep by dep.DependentTask into newGroup
                                      let depList = (from dep in newGroup
                                                     select dep.DependsOnTask).Order()
                                    select new {KeyDep=newGroup.Key,Value=depList};
        var listWithoutDuplicetes = (from dep in listOfGroupDependencies
                      select  dep.Value).Distinct();
        
        List<DO.Task> listTask = new List<DO.Task>();
        foreach (var group in listOfGroupDependencies)
        {
            new DO.Task(Config.NextMilestoneId, "description",0, $"M1", true,);
            // הוספת המשימה שלא תלויה
            var nonDependentTask = group.FirstOrDefault(dep => dep?.DependsOnTask == null);
            if (nonDependentTask != null)
            {
                listTask.Add(BO.Task.Read(nonDependentTask?.IdNumberDependence));
                BO.Milestone.Create(0000);

            }
            else
            {
                BO.Task saveTask = BO.Task.Read(nonDependentTask?.IdNumberDependence);
                milestone.Dependencies?.Add(new TaskInList(saveTask.IdTask, saveTask.Description, saveTask.Alias, saveTask.Status));
                listTask.Add(BO.Task.Read(saveTask.IdTask));
            }
        }
    }
    public Milestone Update(int IDMilestone)
    {
        throw new NotImplementedException();
    }
}
