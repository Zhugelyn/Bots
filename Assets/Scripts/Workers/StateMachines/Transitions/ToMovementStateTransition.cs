using UnityEngine;
using Infrastructure;
using Workers.StateMachines.States;

namespace Workers.StateMachines.Transitions
{
    public class ToMovementStateTransition : Transition
    {
        private Worker _worker;
        
        public ToMovementStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            return _worker.IsMove;
        }
    }
}