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
            if (_worker.IsMove || _worker.HasResource)
                return false;
            
            return _worker.DestinationPoint == _worker.Base.transform.position;
        }
    }
}
