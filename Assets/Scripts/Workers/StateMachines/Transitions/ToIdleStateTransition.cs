using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.Transitions
{
    public class ToIdleStateTransition : Transition
    {
        private float _arrivalThreshold = 1f;
        private Worker _worker;

        public ToIdleStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            bool isNearDestination = Vector3.Distance(_worker.transform.position, _worker.DestinationPoint) < _arrivalThreshold;

            return _worker.DestinationPoint == _worker.BasePosition &&
                   _worker.HasResource == false &&
                   isNearDestination &&
                   _worker.IsSentToBuild == false;
        }
    }
}
