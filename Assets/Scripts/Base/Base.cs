using System.Collections.Generic;
using UnityEngine;
using Workers;
using Workers.Factory;

public class Base : MonoBehaviour
{
    private const int BuildNewBaseCost = 5;
    private const int CreateWorkerCost = 3;

    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private ResourceProvider _resourceProvider;
    [SerializeField] private MeshRenderer _colorPart;
    [SerializeField] private bool _isStartBase;
    [SerializeField] private BaseBuilder _baseBuilder;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private BotRetriever _botRetriever;
    
    private List<Worker> _workers;
    private Flag _flag;
    private Mode _mode;
    
    [field: SerializeField] public Transform GatheringPointWorkers { get; private set; }
    [field: SerializeField] public ParticleSystem Particle { get; private set; }
    [field: SerializeField] public ResourcesCounter ResourceCounter { get; private set; }
    public Color MainColor { get; private set; }
    public bool HasFlag => _flag != null;

    private void Awake()
    {
        _workers = new List<Worker>();
        MainColor = ColorExtension.GetRandomColor();
        _colorPart.material.color = MainColor;
        _mode = Mode.CreateWorkers;
    }

    private void OnEnable()
    {
        if (_workerCreator != null)
            _workerCreator.WorkerCreated += OnWorkerCreated;

        if (_scanner != null)
            _scanner.ResourcesFound += OnResourcesFound;

        if (_botRetriever != null)
            _botRetriever.WorkerArrived += OnWorkerArrived;

        if (_baseBuilder != null)
            _baseBuilder.Built += OnBaseBuilt;
    }

    private void OnDisable()
    {
        if (_workerCreator != null)
            _workerCreator.WorkerCreated -= OnWorkerCreated;

        if (_scanner != null)
            _scanner.ResourcesFound -= OnResourcesFound;

        if (_botRetriever != null)
            _botRetriever.WorkerArrived -= OnWorkerArrived;

        if (_baseBuilder != null)
            _baseBuilder.Built -= OnBaseBuilt;
    }

    private void Start()
    {
        if (_isStartBase)
            _workerCreator.CreateStartWorkers();
    }

    public void SetResourceProvider(ResourceProvider resourceProvider)
    {
        _resourceProvider = resourceProvider;
    }

    public void SetScanner(Scanner scanner)
    {
        _scanner = scanner;
        _scanner.ResourcesFound += OnResourcesFound;
    }

    public void SetFlag(Flag flag)
    {
        _flag = flag;
        TryEnterBuildMode();
    }

    public void RemoveFlag()
    {
        if (_flag != null)
        {
            Destroy(_flag.gameObject);
            _flag = null;
        }

        _mode = Mode.CreateWorkers;
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        TryEnterBuildMode();
    }

    private void OnWorkerCreated(Worker worker)
    {
        _workers.Add(worker);
        TryEnterBuildMode();
        TryAssignTask(worker);
    }

    public void RemoveWorker(Worker worker)
    {
        _workers.Remove(worker);
    }

    private void TryEnterBuildMode()
    {
        if (_flag == null)
            return;

        if (_workers.Count > 1)
            _mode = Mode.BuildNewBase;
    }

    private void OnResourcesFound(IReadOnlyList<Resource> resources)
    {
        _resourceProvider.AddResources(resources);
        TryAssignTasksToIdleWorkers();
    }

    private void OnWorkerArrived(Worker worker)
    {
        if (_workers.Contains(worker) == false)
            return;

        if (worker.CarriedResource != null)
        {
            Resource resource = worker.TakeResource();
            ResourceCounter.Add(resource);
            _resourceProvider.RemoveResource(resource);
        }

        TryAssignTask(worker);
    }

    private void OnBaseBuilt(Base newBase, Worker worker)
    {
        newBase.SetResourceProvider(_resourceProvider);
        newBase.SetScanner(_scanner);
        RemoveWorker(worker);
        worker.SetHomeBase(newBase.GatheringPointWorkers);
        newBase.AddWorker(worker);
        worker.ReturnToBase();
    }

    private void TryAssignTasksToIdleWorkers()
    {
        foreach (var worker in _workers)
        {
            if (worker.IsBusy == false)
                TryAssignTask(worker);
        }
    }

    private void TryAssignTask(Worker worker)
    {
        if (TryAssignBuildTask(worker))
            return;

        TryCreateWorker();

        if (_resourceProvider == null)
        {
            worker.StopMission();
            return;
        }

        if (_resourceProvider.TryGetFreeResource(out Resource resource))
            worker.SendToCollect(resource);
        else
            worker.StopMission();
    }

    private bool TryAssignBuildTask(Worker worker)
    {
        if (_mode != Mode.BuildNewBase)
            return false;

        if (_flag == null)
            return false;

        if (_workers.Count <= 1)
            return false;

        if (ResourceCounter.TryCost(BuildNewBaseCost) == false)
            return false;

        Vector3 buildPosition = _flag.transform.position;
        RemoveFlag();

        worker.BuildCompleted += OnWorkerBuildCompleted;
        worker.SendToBuild(buildPosition);
        return true;
    }

    private void OnWorkerBuildCompleted(Worker worker, Vector3 position)
    {
        worker.BuildCompleted -= OnWorkerBuildCompleted;
        _baseBuilder.Build(worker, position);
    }

    private void TryCreateWorker()
    {
        if (_mode != Mode.CreateWorkers)
            return;

        if (ResourceCounter.TryCost(CreateWorkerCost) == false)
            return;

        _workerCreator.Create();
    }
}

public enum Mode
{
    BuildNewBase,
    CreateWorkers
}
