using System;
using Infrastructure;
using UnityEngine;
using Workers.Factory;

namespace Workers
{
    [RequireComponent(typeof(WorkerStateMachineFactory))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] private int _speed;
        
        private int _maxSpeed = 10;
        private int _minSpeed = 0;
        
        private StateMachine _stateMachine;
        
        public event Action<Vector3> BuildStarted;

        [field: SerializeField] public Transform ResourceCarryPoint { get; private set; }
        [field: SerializeField] public Vector3 DestinationPoint { get; private set; }
        [field: SerializeField] public ResourceDiscovery ResourceDiscovery { get; private set; }
        [field: SerializeField] public WorkerAnimation Animation { get; private set; }
        public bool IsBusy { get; private set; }
        public bool IsMove { get; private set; }
        public Vector3 BasePosition { get; private set; }
        public Resource Resource { get; private set; }
        public Mover Mover { get; private set; }
        public WorkerRole Role { get; private set; }
        public bool IsBuild { get; private set; }
        public bool IsSentToBuild { get; private set; }
        public bool HasResource => Resource != null;

        public int Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed);
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void Initialize(Vector3 basePosiiton)
        {
            _speed = _minSpeed;
            transform.position = basePosiiton;
            BasePosition = basePosiiton;
            IsBusy = false;
            IsMove = false;
            IsBuild = false;
            IsSentToBuild = false;
            Role = WorkerRole.Collector;
            _stateMachine = GetComponent<WorkerStateMachineFactory>().Create(this);

            ResourceDiscovery.Initialize(this);

            Mover = new Mover(transform);
        }

        public void AssignTask() =>
            IsBusy = true;

        public void CompleteTask()
        {
            IsBusy = false;
            IsMove = false;
        }

        public void PickUpResource(Resource resource)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            IsMove = false;
            Resource = resource;
        }

        public void DropResource()
        {
            Resource = null;
            IsMove = false;
        }

        public void BuildAt(Vector3 position)
        {
            DestinationPoint = position;
            Role = WorkerRole.Builder;
            IsSentToBuild = true;
            IsMove = true;
        }

        public void ReserveBuilder()
        {
            Role = WorkerRole.Builder;
        }

        public void SetBuildStatus(bool isBuild)
        {
            IsMove = false;
            IsBuild = isBuild;
        }

        public void StartBuilding()
        {
            BuildStarted?.Invoke(DestinationPoint);
        }

        public void SetDestinationPoint(Vector3 point)
        {
            var offsetY = new Vector3(0, point.y, 0);

            DestinationPoint = point - offsetY;
            IsMove = true;
        }

        public void SetBase(Vector3 basePosition)
        {
            BasePosition = basePosition;
            Role = WorkerRole.Collector;
            IsBusy = false;
            IsMove = true;
            IsSentToBuild = false;
            IsBuild = false;
            DestinationPoint = basePosition;
        }
    }
}

public enum WorkerRole
{
    Builder,
    Collector
}
