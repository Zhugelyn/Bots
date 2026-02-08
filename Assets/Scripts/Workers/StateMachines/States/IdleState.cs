using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.States
{
    public class IdleState : State
    {
        private Worker _worker;

        public IdleState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        public override void Enter()
        {
            _worker.Animation.Idle();
            _worker.CompleteTask();
            _worker.Speed = 0;
        }
    }
}