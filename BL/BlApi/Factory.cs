namespace BlApi;
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();

}

    public int AddTask(BO.Task task)
    {
        DO.Task doTask = new DO.Task(
            task.Id,
            task.Description!,
            task.Alias!,
            false,
            task.Deliverables!,
            (DO.EngineerExperience)task.ComplexityTask,
            task.RequiredEffortTime,
            task.CreatedAtDate,
            task.StartDate,
            task.ScheduledDate,
            task.DeadlineDate,
            task.CompleteDate,
            task.Remarks,
            task.Engineer!.Id);
        try
        {
            int idNewTask = _dal.Task.Create(doTask);
            return idNewTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex)
    ;
        }
    }
    public void RemoveTask(int id)
    {
        DO.Task? task = _dal.Task.Read(id);
        if (task != null)
        {
            _dal.Task.Delete(id);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
    }
    public void UpdateTask(BO.Task task)
    {
        DO.Task? doTask = _dal.Task.Read(task.Id);
        if (doTask != null)
        {
            _dal.Task.Update(doTask);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={task.Id} does Not exist");
        }
    }


}