using Infrastructure;
using UnityEngine;

namespace Workers.StateMachines.States
{
    public class BuildState : State
    {
        private Worker _worker;

        public BuildState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        public override void Enter()
        {
            _worker.Animation.Build();
            _worker.SetBuildStatus(true);
            _worker.Speed = 0;
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