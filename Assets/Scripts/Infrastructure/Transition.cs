namespace Infrastructure
{
    public abstract class Transition
    {
        private State _nextState;

        public Transition(State nextState)
        {
            _nextState = nextState;
        }

        public bool TryTransit(out State nextState)
        {
            nextState = _nextState;

            return CanTransit();
        }

        protected abstract bool CanTransit();
    }
}