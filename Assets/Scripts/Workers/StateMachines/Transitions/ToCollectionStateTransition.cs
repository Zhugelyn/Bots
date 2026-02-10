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
            return _worker.DestinationPoint == _worker.transform.position && 
                   _worker.IsBusy &&
                   _worker.HasResource &&
                   _worker.DestinationPoint != _worker.BasePosition;
        }
    }
}