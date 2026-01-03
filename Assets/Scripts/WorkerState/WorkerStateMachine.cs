using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStateMachine : MonoBehaviour
{
    private Worker _worker;
    
    private IdleState _idleState;
    private MovementState _movementState;
    private CollectionState _collectionState;
    private WorkerStateContext _context;

    public void Initialize(Worker worker)
    {
        _worker = worker;
        _idleState = gameObject.AddComponent<IdleState>();
        _movementState = gameObject.AddComponent<MovementState>();
        _collectionState = gameObject.AddComponent<CollectionState>();
        _context = new WorkerStateContext(worker);
    }
    
    
    public void SetIdleState() =>
        _context.Transition(_idleState);

    public void SetCollectionState(Resource resource) =>
        _context.Transition(_collectionState, resource);

    public void SetMovementState(Vector3 destinationPoint)
    {
        var offsetY = new Vector3(0, destinationPoint.y, 0);
        _worker.DestinationPoint = destinationPoint - offsetY;
        _context.Transition(_movementState);
    }
}
