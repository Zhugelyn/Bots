using System;
using UnityEngine;

[RequireComponent(typeof(WorkerAnimation))]
[RequireComponent(typeof(WorkerStateMachine))]
[RequireComponent(typeof(ResourceDiscovery))]
public class Worker : MonoBehaviour
{
    [field: SerializeField] public Transform ResourcePosition { get; private set; }

    private int _speed;
    private int _maxSpeed;
    private int _minSpeed;
    
    private ResourceDiscovery _resourceDiscovery;

    public bool IsBusy { get; set; }
    public Resource Resource { get; set; }
    public Vector3 DestinationPoint { get; set; }
    public WorkerStateMachine StateMachine { get; private set; }
    public WorkerAnimation Animation { get; private set; }
    public Base Base { get; private set; }

    public int  Speed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed);
    }

    private void Awake()
    {
        Animation = GetComponent<WorkerAnimation>();
        StateMachine = GetComponent<WorkerStateMachine>();
        _resourceDiscovery = GetComponent<ResourceDiscovery>();
    }

    public void Initialize(Base @base, Vector3 position)
    {
        _maxSpeed = 10;
        _minSpeed = 0;
        _speed = _minSpeed;
        transform.position = position;
        Base = @base;
        IsBusy = false;
        
        StateMachine.Initialize(this);
        _resourceDiscovery.Initialize(this);
        _resourceDiscovery.Discovered += OnResourceDiscovered;
    }
    
    private void OnDisable()
    {
        _resourceDiscovery.Discovered -= OnResourceDiscovered;
    }
    
    private void OnResourceDiscovered(Resource resource) =>
        StateMachine.SetCollectionState(resource);
}
