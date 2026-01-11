using Infrastructure;
using UnityEngine;
using Workers.StateMachines.States;
using Workers.StateMachines.Transitions;

namespace Workers.Factory
{
    public class WorkerStateMachineFactory : MonoBehaviour
    {
        public void Create(Worker worker)
        {
            IStateChanger stateMachines = new StateMachine();

            var collectionState = new CollectionState(stateMachines, worker);
            var idleState = new IdleState(stateMachines, worker);
            var movementState = new MovementState(stateMachines, worker);

            var toIdle = new ToCollectionStateTransition(movementState, worker);
            var idleStateTransition = new ToIdleStateTransition(movementState, worker);
        }
    }
}