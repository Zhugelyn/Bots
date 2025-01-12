using UnityEngine;

public class IdleState : MonoBehaviour, IWorkerState
{
    private Worker _worker;

    public void Initialize(Worker worker, ICollectable _)
    {
        if (_worker == null)
            _worker = worker;
        
        worker.IsBusy = false;
        worker.Resource = null;
        worker.Speed = 0;
    }

    private void Update()
    {
        if (_worker != null && _worker.IsBusy == false)
            _worker.Animation.Idle();
    }
}

