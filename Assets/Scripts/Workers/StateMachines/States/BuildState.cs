using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.States
{
    public class BuildState : State
    {
        private Worker _worker;
        private float _animationDeltaY = 1.6f;

        public BuildState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        public override void Enter()
        {
            _worker.transform.position = new Vector3(_worker.transform.position.x, _worker.transform.position.y + _animationDeltaY, _worker.transform.position.z);
            _worker.Animation.Build();
            _worker.SetBuildStatus(true);
            _worker.Speed = 0;
            _worker.StartBuilding();
        }

        public override void Exit()
        {
            
        }

        public void FinishBuild()
        {
            _worker.SetBuildStatus(false);
        }
    }
}