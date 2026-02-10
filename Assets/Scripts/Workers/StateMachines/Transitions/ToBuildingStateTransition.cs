using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.Transitions
{
    public class ToBuildingStateTransition : Transition
    {
        private Worker _worker;
        private float _threshold = 10f;

        public ToBuildingStateTransition(State nextState, Worker worker) : base(nextState)
        {
            _worker = worker;
        }

        protected override bool CanTransit()
        {
            float distance = Vector3.Distance(_worker.transform.position, _worker.DestinationPoint);

            return distance <= _threshold &&
                   _worker.IsSentToBuild;
        }
    }
}