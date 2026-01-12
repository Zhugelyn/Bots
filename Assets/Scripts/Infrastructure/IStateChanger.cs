namespace Infrastructure
{
    public interface IStateChanger
    {
        void ChangeState(State state);
    }
}