using UnityEngine;
using Infrastructure;

namespace Workers.StateMachines.States
{
    public class MovementState : State
    {
        private int _speedWithoutResource = 5;
        private int _speedWithResource = 3;
        private Worker _worker;

        public MovementState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        protected override void OnUpdate()
        {
            _worker.transform.position = Vector3.MoveTowards(_worker.transform.position,
                _worker.DestinationPoint, _worker.Speed * Time.deltaTime);
            _worker.transform.root.LookAt(_worker.DestinationPoint);
        }

        public override void Enter()
        {
            _worker.ResourceDiscovery.Discovered += PickUpResource;
            _worker.Animation.Move();
            _worker.AssignTask();
            _worker.Speed = _worker.HasResource ? _speedWithResource : _speedWithoutResource;
        }

        public override void Exit()
        {
            _worker.ResourceDiscovery.Discovered -= PickUpResource;
        }

        private void PickUpResource(Resource resource)
        {
            _worker.PickUpResource(resource);
        }
    }
}