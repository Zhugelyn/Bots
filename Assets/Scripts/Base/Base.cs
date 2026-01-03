using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCommander)),
RequireComponent(typeof(ResourceReceiver)),
RequireComponent(typeof(ResourceRepository)),
RequireComponent(typeof(Scanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;
    
    private List<Worker> _workers;

    public void Initialize()
    {
        _workers = new List<Worker>();
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
    }

    private void AddWorker(Worker worker) => 
        _workers.Add(worker);

    public Worker GetFreeWorker() => 
        _workers.FirstOrDefault(worker => !worker.IsBusy);
}