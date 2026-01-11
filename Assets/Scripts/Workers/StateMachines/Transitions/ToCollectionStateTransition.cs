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
            return _worker.Resource is not null && 
                   _worker.DestinationPoint == _worker.Resource.transform.position;
        }
    }
}