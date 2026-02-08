using Infrastructure;

namespace Workers.StateMachines.Transitions
{
    public class ToBuildingStateTransition : Transition
    {
        private Worker _worker;
        
        public ToBuildingStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            return _worker.DestinationPoint == _worker.transform.position &&
                   _worker.Role == WorkerRole.Builder;
        }
    }
}
