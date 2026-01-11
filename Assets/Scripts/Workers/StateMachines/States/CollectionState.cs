using UnityEngine;
using Infrastructure;

namespace Workers.StateMachines.States
{
    public class CollectionState : State
    {
        private Worker _worker;
        private Resource _resource;

        public CollectionState(IStateChanger stateChanger, Worker worker, Resource resource) : base(stateChanger)
        {
            _worker = worker;
            _resource = resource;
        }

        public override void Enter()
        {
            _worker.Speed = 0;
            _worker.Animation.PickUp();
        }

        public override void Exit()
        {
            _worker.PickUpResource(_resource);
            _worker.Resource.UpdateState(_worker.ResourcePosition);
        }
    }
}