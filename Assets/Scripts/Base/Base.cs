using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workers;
using Workers.Factory;


[RequireComponent(typeof(ResourceReceiver))]
[RequireComponent(typeof(ResourcesCounter))]
public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private ResourceTaskQueue _resourceTaskQueue;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private MeshRenderer _colorPart;

    private List<Worker> _workers;
    private BaseCommander _commander;

    [field: SerializeField] public Transform GatheringPointWorkers { get; private set; }
    public ResourceReceiver ResourceReceiver { get; private set; }
    public ResourcesCounter ResourceCounter { get; private set; }
    public BaseTaskQueue TaskQueue { get; private set; }
    public Color MainColor { get; private set; }
    public bool HasFlag { get; private set; }
    public Flag Flag { get; private set; }
    public Mode Mode { get; private set; }

    private void Awake()
    {
        ResourceReceiver = GetComponent<ResourceReceiver>();
        ResourceCounter = GetComponent<ResourcesCounter>();
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
        _commander.AssignTask(GetWorker(false, WorkerRole.Collector));
    }

    public void Initialize()
    {
        _workers = new List<Worker>();
        _commander = new BaseCommander(_resourceTaskQueue);
        TaskQueue = new BaseTaskQueue();
        MainColor = ColorExtension.GetRandomColor();
        _colorPart.material.color = MainColor;
        Mode = Mode.CreateWorkers;
    }

    public void Setflag(Flag flag)
    {
        HasFlag = true;
        Flag = flag;
        Mode = Mode.BuildNewBase;
    }

    public void RemoveFlag()
    {
        Destroy(Flag.gameObject);
        HasFlag = false;
        Flag = null;
        Mode = Mode.CreateWorkers;
    }

    public bool TryBuildNewBase()
    {
        if (HasFlag == false)
            return false;

        var worker = GetWorker(WorkerRole.Builder);

        if (worker != null && worker.IsBusy == false)
        {
            _commander.BuildNewBase(worker, Flag.transform.position);
            return true;
        }

        worker = _workers.FirstOrDefault();

        if (worker != null)
            worker.ReserveBuilder();

        return false;
    }

    public bool TryCreateWorker()
    {
        _workerCreator.Create();
        return true;
    }

    public int GetWorkerCount()
    {
        return _workers.Count;
    }
    
    private Worker GetWorker(WorkerRole role) =>
        _workers.FirstOrDefault(worker => role == worker.Role);

    private Worker GetWorker(bool isBusy, WorkerRole role) =>
        _workers.FirstOrDefault(worker => worker.IsBusy == isBusy && role == worker.Role);

    private void AddWorker(Worker worker) =>
        _workers.Add(worker);
}

public enum Mode
{
    BuildNewBase,
    CreateWorkers
}