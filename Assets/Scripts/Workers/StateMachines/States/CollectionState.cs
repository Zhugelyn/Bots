using UnityEngine;
using Infrastructure;

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
            _worker.Animation.PickUpFinished += SetDestination;
        }

        public override void Exit()
        {
            _worker.Animation.PickUpFinished -= SetDestination;
            _worker.Resource.UpdateState(_worker.ResourcePosition);
        }

        public void SetDestination()
        {
            _worker.SetDestinationPoint(_worker.Base.transform.position);
        }
    }
}