using UnityEngine;
using Infrastructure;

namespace Workers.StateMachines.Transitions
{
    public class ToCollectionStateTransition : Transition
    {
        private Worker _worker;

        public ToCollectionStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            var offsetY = new Vector3(0, 0, 0);
            
            if (_worker.Resource is not null)
                offsetY = new Vector3(0, _worker.Resource.transform.position.y, 0);
            
            return _worker.Resource is not null && 
                   _worker.DestinationPoint == _worker.Resource.transform.position - offsetY;
        }
    }
}