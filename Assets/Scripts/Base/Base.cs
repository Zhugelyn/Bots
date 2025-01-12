using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;

    private List<Worker> _workers;
    private Scanner _scanner;
    private ResourceReceiver _resourceReceiver;
    private BaseCommander _baseCommander;

    public List<Vector3> _backlog;
    public ResourceReceiver ResourceReceiver => _resourceReceiver; 
    public List<Worker> Workers => _workers;

    public void Initialize()
    {
        _workers = new List<Worker>();
        _backlog = new List<Vector3>();
        _scanner = GetComponent<Scanner>();

        _resourceReceiver = gameObject.AddComponent<ResourceReceiver>();
        _scanner = gameObject.AddComponent<Scanner>();
        _baseCommander = gameObject.AddComponent<BaseCommander>();
        
        _scanner.ResourcesFound += AddResources;

        _baseCommander.Initialize(this);
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        _workerCreator.WorkerCreated += AddWorker;
    }

    private void OnDisable()
    {
        _workerCreator.WorkerCreated -= AddWorker;
        _scanner.ResourcesFound -= AddResources;
    }

    private void AddWorker(Worker worker) => _workers.Add(worker);

    private void AddResources(List<Vector3> resources)
    {
        foreach (var resourcePosition in resources)
            _backlog.Add(resourcePosition);
    }
}