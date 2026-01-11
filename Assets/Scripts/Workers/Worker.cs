using System;
using UnityEngine;

namespace Workers
{
    [RequireComponent(typeof(WorkerAnimation))]
    [RequireComponent(typeof(ResourceDiscovery))]
    public class Worker : MonoBehaviour
    {
        [field: SerializeField] public Transform ResourcePosition { get; private set; }

        private int _speed;
        private int _maxSpeed;
        private int _minSpeed;

        private ResourceDiscovery _resourceDiscovery;

        public bool IsBusy { get; private set; }
        public Resource Resource { get; private set; }
        public Vector3 DestinationPoint { get; private set; }
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
            
            _resourceDiscovery.Initialize(this);
        }

        public void AssignTask() =>
            IsBusy = true;

        public void CompleteTask() =>
            IsBusy = false;

        public void PickUpResource(Resource resource)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            Resource = resource;
        }

        public void DropResource()
        {
            Resource = null;
        }

        public void SetDestinationPoint(Vector3 point)
        {
            DestinationPoint = point;
        }
    }
}
