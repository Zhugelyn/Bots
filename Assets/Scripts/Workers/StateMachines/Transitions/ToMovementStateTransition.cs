using UnityEngine;
using Infrastructure;
using Workers.StateMachines.States;

namespace Workers.StateMachines.Transitions
{
    public class ToMovementStateTransition : Transition
    {
        private const float ReachedThreshold = 1f;
        
        private Worker _worker;
        
        public ToMovementStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            float distance = Vector3.Distance(_worker.transform.position, _worker.DestinationPoint);
            return distance <= ReachedThreshold;
        }
    }
}