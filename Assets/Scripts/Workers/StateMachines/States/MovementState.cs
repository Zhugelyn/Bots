using UnityEngine;
using Infrastructure;

namespace Workers.StateMachines.States
{
    public class MovementState : State
    {
        private int _speedWithoutResource = 5;
        private int _speedWithResource = 3;
        private Worker _worker;

        public MovementState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        protected override void OnUpdate()
        {
            _worker.Mover.MoveTo(_worker.DestinationPoint, _worker.Speed);
        }

        public override void Enter()
        {
            _worker.Animation.Move();
            _worker.AssignTask();
            _worker.Speed = _worker.HasResource ? _speedWithResource * 4 : _speedWithoutResource * 4;
        }
    }
}