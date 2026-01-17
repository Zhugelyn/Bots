using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workers;
using Workers.Factory;


[RequireComponent(typeof(ResourceReceiver))]
public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private ResourceTaskQueue _resourceTaskQueue;
    [SerializeField] private Scanner _scanner;
    
    private List<Worker> _workers;
    private BaseCommander _commander;

    [field: SerializeField] public Transform GatheringPointWorkers { get; private set; }
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

    private void Update()
    {
        _commander.AssignTask(GetFreeWorker());
    }

    public void Initialize()
    {
        _workers = new List<Worker>();
        _commander = new BaseCommander(_resourceTaskQueue);
    }

    private Worker GetFreeWorker() => 
        _workers.FirstOrDefault(worker => !worker.IsBusy);
    
    private void AddWorker(Worker worker) => 
        _workers.Add(worker);
}