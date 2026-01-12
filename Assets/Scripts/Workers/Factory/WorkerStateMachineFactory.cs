using Infrastructure;
using UnityEngine;
using Workers.StateMachines.States;
using Workers.StateMachines.Transitions;

namespace Workers.Factory
{
    public class WorkerStateMachineFactory : MonoBehaviour
    {
        public StateMachine Create(Worker worker)
        {
            var stateMachines = new StateMachine();

            var collectionState = new CollectionState(stateMachines, worker);
            var idleState = new IdleState(stateMachines, worker);
            var movementState = new MovementState(stateMachines, worker);

            var toCollectionStateTransition = new ToCollectionStateTransition(collectionState, worker);
            var toIdleStateTransition = new ToIdleStateTransition(idleState, worker);
            var toResourceMoveTransition = new ToMovementStateTransition(movementState, worker);
            var toBaseMoveTransition = new ToMovementStateTransition(movementState, worker);
            
            collectionState.AddTransition(toBaseMoveTransition);
            idleState.AddTransition(toResourceMoveTransition);
            movementState.AddTransition(toCollectionStateTransition);
            movementState.AddTransition(toIdleStateTransition);
            
            stateMachines.ChangeState(idleState);
            
            return stateMachines;
        }
    }
}