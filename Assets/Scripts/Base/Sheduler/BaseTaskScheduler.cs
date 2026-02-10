using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTaskScheduler : MonoBehaviour
{
    [SerializeField] private Base _base;

    private int _buildNewBaseCostPerType = 5;
    private int _createWorkerCostPerType = 3;
    
    private void OnEnable()
    {
        _base.ResourceCounter.Changed += OnResourcesChanged;
    }

    private void OnDisable()
    {
        _base.ResourceCounter.Changed -= OnResourcesChanged;
    }

    private void OnResourcesChanged(Dictionary<ResourceType, int> resources)
    {
        if (_base == null || _base.ResourceCounter == null || _base.TaskQueue == null)
            return;
        
        if (_base.Mode == Mode.BuildNewBase)
            TryScheduleBuildNewBase(resources);
        
        if (_base.Mode == Mode.CreateWorkers)
            TryScheduleCreateWorker(resources);
    }

    private void TryScheduleBuildNewBase(Dictionary<ResourceType, int> resources)
    {
        if (HasEnoughEachKnownType(resources, _buildNewBaseCostPerType) == false)
            return;

        if (_base.TaskQueue.HasTaskOfType<BuildBaseTask>())
            return;

        _base.TaskQueue.AddTask(new BuildBaseTask(TaskPriority.High, _base));
    }

    private void TryScheduleCreateWorker(Dictionary<ResourceType, int> resources)
    {
        if (HasEnoughEachKnownType(resources, _createWorkerCostPerType) == false)
            return;

        if (_base.TaskQueue.HasTaskOfType<CreateWorkerTask>())
            return;

        _base.TaskQueue.AddTask(new CreateWorkerTask(TaskPriority.Normal, _base));
    }

    private static bool HasEnoughEachKnownType(Dictionary<ResourceType, int> resources, int amount)
    {
        return resources != null && resources.Count > 0 && resources.All(resource => resource.Value >= amount);
    }
}
