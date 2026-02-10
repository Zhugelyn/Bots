public abstract class Task
{
    private static int s_Id = 0;
    
    protected Task(TaskPriority priority = TaskPriority.Normal)
    {
        Id = s_Id++;
        Priority = priority;
    }
    
    public int Id { get; private set; }
    public TaskPriority Priority { get; private set; }

    public virtual bool TryExecute()
    {
        return true;
    }
}

public enum TaskPriority
{
    Normal,
    High,
}
