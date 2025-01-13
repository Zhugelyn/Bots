using UnityEngine;

[RequireComponent(typeof(WorkerAnimation))]
public class Worker : MonoBehaviour
{
    public bool IsBusy;
    public Resource Resource;

    private int _speed;
    private int _maxSpeed;
    private int _minSpeed;

    private IdleState _idleState;
    private MovementState _movementState;
    private CollectionState _collectionState;
    private WorkerStateContext _workerStateContext;
    private ResourceDiscovery _resourceDiscovery;

    public void Initialize(Base @base, Vector3 position)
    {
        _maxSpeed = 10;
        _minSpeed = 0;
        _speed = _minSpeed;
        transform.position = position;
        Base = @base;
        IsBusy = false;
        Animation = GetComponent<WorkerAnimation>();

        _resourceDiscovery = gameObject.AddComponent<ResourceDiscovery>();
        _resourceDiscovery.Initialize(this);

        _idleState = gameObject.AddComponent<IdleState>();
        _movementState = gameObject.AddComponent<MovementState>();
        _collectionState = gameObject.AddComponent<CollectionState>();
        _workerStateContext = new WorkerStateContext(this);

        _resourceDiscovery.Discovered += SetCollectionState;
    }

    public Vector3 DestinationPoint { get; private set; }

    public WorkerAnimation Animation { get; private set; }

    public Base Base { get; private set; }

    public int  Speed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed);
    }
    
    private void OnDisable()
    {
        _resourceDiscovery.Discovered -= SetCollectionState;
    }

    public void SetIdleState() =>
        _workerStateContext.Transition(_idleState);

    public void SetCollectionState(Resource resource) =>
        _workerStateContext.Transition(_collectionState, resource);

    public void SetMovementState(Vector3 destinationPoint)
    {
        var offsetY = new Vector3(0, destinationPoint.y, 0);
        DestinationPoint = destinationPoint - offsetY;
        _workerStateContext.Transition(_movementState);
    }
}
