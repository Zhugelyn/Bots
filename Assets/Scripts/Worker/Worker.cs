using UnityEngine;

[RequireComponent(typeof(WorkerAnimation))]
public class Worker : MonoBehaviour
{
    [field: SerializeField] public Transform ResourcePosition { get; private set; }

    public bool IsBusy;
    public Resource Resource;
    public Vector3 DestinationPoint;

    private int _speed;
    private int _maxSpeed;
    private int _minSpeed;
    
    private ResourceDiscovery _resourceDiscovery;

    public WorkerStateMachine StateMachine { get; private set; }
    public WorkerAnimation Animation { get; private set; }

    public Base Base { get; private set; }

    public int  Speed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed);
    }
    
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

        StateMachine = gameObject.AddComponent<WorkerStateMachine>();
        StateMachine.Initialize(this);

        _resourceDiscovery.Discovered += OnResourceDiscovered;
    }
    
    private void OnDisable()
    {
        _resourceDiscovery.Discovered -= OnResourceDiscovered;
    }
    
    private void OnResourceDiscovered(Resource resource) =>
        StateMachine.SetCollectionState(resource);
}
