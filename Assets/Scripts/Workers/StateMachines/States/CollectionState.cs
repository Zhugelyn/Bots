using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.States
{
    public class CollectionState : State
    {
        private Worker _worker;

        public CollectionState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        public override void Enter()
        {
            _worker.Speed = 0;
            _worker.Animation.PickUp();
            _worker.Animation.OnPickUpCompleted += SetDestination;
        }

        public override void Exit()
        {
            _worker.Animation.OnPickUpCompleted -= SetDestination;
            
            if (_worker.Resource != null)
                _worker.Resource.UpdateState(_worker.ResourceCarryPoint);
        }

        private void SetDestination()
        {
            _worker.SetDestinationPoint(_worker.BasePosition);
        }
    }
}