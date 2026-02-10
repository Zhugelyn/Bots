using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTaskScheduler : MonoBehaviour
{
    private const int BuildNewBaseCost = 5;
    private const int CreateWorkerCost = 3;

    [SerializeField] private Base _base;

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
            TryScheduleBuildNewBase();

        if (_base.Mode == Mode.CreateWorkers)
            TryScheduleCreateWorker();
    }

    private void TryScheduleBuildNewBase()
    {
        if (_base.ResourceCounter.HasEnoughTotal(BuildNewBaseCost) == false)
            return;

        if (_base.TaskQueue.HasTaskOfType<BuildBaseTask>())
            return;

        _base.TaskQueue.AddTask(new BuildBaseTask(TaskPriority.High, _base));
    }

    private void TryScheduleCreateWorker()
    {
        if (_base.ResourceCounter.HasEnoughTotal(CreateWorkerCost) == false)
            return;

        if (_base.TaskQueue.HasTaskOfType<CreateWorkerTask>())
            return;

        _base.TaskQueue.AddTask(new CreateWorkerTask(TaskPriority.Normal, _base));
    }
}
