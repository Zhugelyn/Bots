using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workers;
using Workers.Factory;

[RequireComponent(typeof(BaseCommander)),
RequireComponent(typeof(ResourceReceiver)),
RequireComponent(typeof(ResourceRepository)),
RequireComponent(typeof(Scanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;
    
    private List<Worker> _workers;
    
    public ResourceReceiver ResourceReceiver { get; private set; }

    private void Awake()
    {
        ResourceReceiver = GetComponent<ResourceReceiver>();
        Initialize();
    }

    private void OnEnable()
    {
        _workerCreator.WorkerCreated += AddWorker;
    }

    private void OnDisable()
    {
        _workerCreator.WorkerCreated -= AddWorker;
    }
    
    public void Initialize()
    {
        _workers = new List<Worker>();
    }

    private void AddWorker(Worker worker) => 
        _workers.Add(worker);

    public Worker GetFreeWorker() => 
        _workers.FirstOrDefault(worker => !worker.IsBusy);
}