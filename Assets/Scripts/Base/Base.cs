using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workers;
using Workers.Factory;

public class Base : MonoBehaviour
{
    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private ResourceTaskQueue _resourceTaskQueue;
    [SerializeField] private MeshRenderer _colorPart;
    [SerializeField] private bool _isStartBase;
    [SerializeField] private BaseConstructionPresenter _constructionPresenter;
    
    private List<Worker> _workers;
    private BaseCommander _commander;
    
    [field: SerializeField] public Transform GatheringPointWorkers { get; private set; }
    [field: SerializeField] public ParticleSystem Particle { get; private set; }
    [field: SerializeField] public ResourceReceiver ResourceReceiver { get; private set; }
    [field: SerializeField] public ResourcesCounter ResourceCounter { get; private set; }
    public BaseTaskQueue TaskQueue { get; private set; }
    public Color MainColor { get; private set; }
    public bool HasFlag { get; private set; }
    public Flag Flag { get; private set; }
    public Mode Mode { get; private set; }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        if (_workerCreator != null)
            _workerCreator.WorkerCreated += AddWorker;
    }

    private void OnDisable()
    {
        if (_workerCreator != null)
            _workerCreator.WorkerCreated -= AddWorker;
    }

    private void Start()
    {
        if (_isStartBase && _workerCreator != null)
            _workerCreator.CreateStartWorkers();
    }

    private void Update()
    {
        _commander.AssignTask(GetWorker(false, WorkerRole.Collector));
    }

    public void Initialize()
    {
        _workers = new List<Worker>();
        
        if (_resourceTaskQueue == null)
            _resourceTaskQueue = FindObjectOfType<ResourceTaskQueue>();
        
        _commander = new BaseCommander(_resourceTaskQueue);
        TaskQueue = new BaseTaskQueue();
        MainColor = ColorExtension.GetRandomColor();
        _colorPart.material.color = MainColor;
        Mode = Mode.CreateWorkers;
    }

    public void SetFlag(Flag flag)
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

        if (_workers.Count <= 1)
            return false;

        var worker = GetWorker(WorkerRole.Builder);

        if (worker != null && worker.IsBusy == false)
        {
            if (_constructionPresenter != null)
            {
                _constructionPresenter.SetWorker(worker);
                _constructionPresenter.OnConstructionComplete -= OnBaseConstructionComplete;
                _constructionPresenter.OnConstructionComplete += OnBaseConstructionComplete;
            }
            
            _commander.BuildNewBase(worker, Flag.transform.position);
            Mode = Mode.CreateWorkers;
            return true;
        }

        worker = _workers.FirstOrDefault();

        if (worker != null)
            worker.ReserveBuilder();

        return false;
    }
    
    private void OnBaseConstructionComplete(Base newBase, Worker worker)
    {
        RemoveWorker(worker);
        worker.SetBase(newBase.GatheringPointWorkers.position);
        newBase.AddWorker(worker);
        RemoveFlag();
        
        var resourceCreators = FindObjectsOfType<ResourceCreator>();
        
        if (newBase.ResourceReceiver != null)
        {
            foreach (var creator in resourceCreators)
            {
                if (creator != null)
                    creator.RegisterReceiver(newBase.ResourceReceiver);
            }
        }
        
        if (_constructionPresenter != null)
            _constructionPresenter.OnConstructionComplete -= OnBaseConstructionComplete;
    }

    public bool TryCreateWorker()
    {
        _workerCreator.Create();
        return true;
    }

    public void AddWorker(Worker worker) =>
        _workers.Add(worker);

    public void RemoveWorker(Worker worker) =>
        _workers.Remove(worker);
    
    private Worker GetWorker(WorkerRole role) =>
        _workers.FirstOrDefault(worker => role == worker.Role);

    private Worker GetWorker(bool isBusy, WorkerRole role) =>
        _workers.FirstOrDefault(worker => worker.IsBusy == isBusy && role == worker.Role);
}

public enum Mode
{
    BuildNewBase,
    CreateWorkers
}