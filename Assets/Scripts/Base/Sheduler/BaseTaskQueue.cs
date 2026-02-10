using System;
using System.Collections.Generic;
using System.Linq;

public class BaseTaskQueue
{
    private List<Task> _tasks;

    public event Action OnChange;

    public BaseTaskQueue()
    {
        _tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
        OnChange?.Invoke();
    }

    public void RemoveTask(Task task)
    {
        _tasks.Remove(task);
        OnChange?.Invoke();
    }

    public Task GetMostPriorityTasks()
    {
        var task = _tasks.FirstOrDefault(task => task.Priority == TaskPriority.High);
        
        if (task == null)
            task = _tasks.OrderBy(task => task.Id).FirstOrDefault();
        
        return task; 
    }

    public bool HasTaskOfType<T>() where T : Task
    {
        return _tasks.Any(task => task is T);
    }
}
