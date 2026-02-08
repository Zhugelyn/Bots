using UnityEngine;
using Workers;

public class BaseCommander
{
    private ResourceTaskQueue _taskQueue;

    public BaseCommander(ResourceTaskQueue resourceTaskQueue)
    {
        _taskQueue = resourceTaskQueue;
    }

    public void AssignTask(Worker worker)
    {
        if (_taskQueue.HasTasks == false || worker == null)
            return;

        Vector3 position = _taskQueue.GetNext();
        worker.SetDestinationPoint(position);
    }

    public void BuildNewBase(Worker worker, Vector3 buildPosition)
    {
        worker.BuildAt(buildPosition);
    }
}