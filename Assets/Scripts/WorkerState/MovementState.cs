using UnityEngine;

public class MovementState : MonoBehaviour, IWorkerState
{
    private int _speedWithoutResource = 5;
    private int _speedWithResource = 3;
    private Worker _worker;

    public void Initialize(Worker worker, ICollectable _)
    {
        if (_worker == null)
            _worker = worker;

        _worker.IsBusy = true;
        _worker.Speed = _worker.Resource == true ? _speedWithResource : _speedWithoutResource;
    }

    private void Update()
    {
        if (_worker != null && _worker.Speed > 0)
            Move();
    }

    private void Move()
    { 
        _worker.Animation.Move();

        transform.position = Vector3.MoveTowards(transform.position,
            _worker.DestinationPoint, _worker.Speed * Time.deltaTime);
        transform.root.LookAt(_worker.DestinationPoint);
    }
}
