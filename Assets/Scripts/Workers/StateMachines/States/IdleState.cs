using Infrastructure;

namespace Workers.StateMachines.States
{
    public class IdleState : State
    {
        private Worker _worker;

        public IdleState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
        {
            _worker = worker;
        }

        public void Initialize(Worker worker, ICollectable _)
        {
            if (_worker == null)
                _worker = worker;

            worker.CompleteTask();
            worker.DropResource();
            worker.Speed = 0;
        }

        private void Update()
        {
            if (_worker != null && _worker.IsBusy == false)
                _worker.Animation.Idle();
        }
    }
}