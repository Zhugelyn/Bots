using Infrastructure;

namespace Workers.StateMachines.Transitions
{
    public class ToIdleStateTransition : Transition
    {
        private Worker _worker;

        public ToIdleStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            return _worker.DestinationPoint == _worker.BasePosition && 
                   _worker.HasResource == false &&
                   _worker.IsBusy == false;
        }
    }
}
