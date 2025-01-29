using UnityEngine;

public class CollectionState : MonoBehaviour, IWorkerState
{
    private Worker _worker;
    private Resource _resource;
    
    public void Initialize(Worker worker, ICollectable resource)
    {
        if (_worker is null)
            _worker = worker;

        _worker.Speed = 0;
        _resource = (Resource)resource;
        _worker.Animation.PickUpFinished += SetMovementState;
        
        _worker.Animation.PickUp();
    }

    private void SetMovementState()
    {
        _worker.Resource = _resource;
        _worker.SetMovementState(_worker.Base.transform.position);
    }
}
