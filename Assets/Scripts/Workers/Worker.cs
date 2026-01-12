using System;
using Infrastructure;
using UnityEngine;
using Workers.Factory;

namespace Workers
{
    [RequireComponent(typeof(WorkerAnimation))]
    [RequireComponent(typeof(ResourceDiscovery))]
    [RequireComponent(typeof(WorkerStateMachineFactory))]
    public class Worker : MonoBehaviour
    {
        [field: SerializeField] public Transform ResourceCarryPoint { get; private set; }

        private int _speed;
        private int _maxSpeed;
        private int _minSpeed;
        private StateMachine _stateMachine;
        
        public bool HasResource => Resource != null;
        
        public bool IsBusy { get; private set; }
        public bool IsMove { get; private set; }
        public Vector3 DestinationPoint { get; private set; }
        public Resource Resource { get; private set; }
        public ResourceDiscovery ResourceDiscovery { get; private set; } 
        public WorkerAnimation Animation { get; private set; }
        public Base Base { get; private set; }

        public int Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, _minSpeed, _maxSpeed);
        }

        private void Awake()
        {
            Animation = GetComponent<WorkerAnimation>();
            ResourceDiscovery = GetComponent<ResourceDiscovery>();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void Initialize(Base @base, Vector3 position)
        {
            _maxSpeed = 10;
            _minSpeed = 0;
            _speed = _minSpeed;
            transform.position = position;
            Base = @base;
            IsBusy = false;
            IsMove = false;
            _stateMachine = GetComponent<WorkerStateMachineFactory>().Create(this);
            
            ResourceDiscovery.Initialize(this);
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

        public void SetDestinationPoint(Vector3 point)
        {
            var offsetY = new Vector3(0, point.y, 0);
            
            DestinationPoint = point - offsetY;
            IsMove = true;
        }
    }
}
