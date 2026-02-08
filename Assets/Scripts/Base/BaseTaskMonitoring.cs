using System.Collections;
using UnityEngine;

public class BaseTaskMonitoring : MonoBehaviour
{
    [SerializeField] private Base _base;
    
    private float _checkInterval = 5f;
    private Coroutine _coroutine;
    private Task _currentTask;

    private void OnEnable()
    {
        _base.TaskQueue.OnChange += StartMonitoring;
    }

    private void OnDisable()
    {
        _base.TaskQueue.OnChange -= StartMonitoring;
    }

    public void StartMonitoring()
    {
        if (_coroutine != null)
            return;

        if (_currentTask == null)
        {
            _currentTask = _base.TaskQueue.GetMostPriorityTasks();
            
            Debug.Log(_currentTask);
            
            if (_currentTask == null)
                return;
        }

        _coroutine = StartCoroutine(MonitorCoroutine());
    }

    private IEnumerator MonitorCoroutine()
    {
        while (_currentTask.TryRun() == false)
        {
            yield return new WaitForSeconds(_checkInterval);
        }
        
        _base.TaskQueue.RemoveTask(_currentTask);
        _currentTask = null;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
