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
        }

        public override void Exit()
        {
            _worker.Resource.UpdateState(_worker.ResourcePosition);
        }
    }
}