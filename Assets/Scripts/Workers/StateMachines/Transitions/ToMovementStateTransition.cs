using Infrastructure;
using Workers.StateMachines.States;

namespace Workers.StateMachines.Transitions
{
    public class ToMovementStateTransition : Transition
    {
        private Worker _worker;

        public ToMovementStateTransition(MovementState nextState) : base(nextState)
        {
        }

        protected override bool CanTransit()
        {
            return true;
        }
    }
}