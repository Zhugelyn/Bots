
public class WorkerStateContext
{ 
    private readonly Worker _worker;

    public WorkerStateContext(Worker worker)
    {
        _worker = worker;
    }

    public IWorkerState CurrentState { get; set; }

    public void Transition(IWorkerState state, ICollectable resource = null)
    {
        CurrentState = state;
        CurrentState.Initialize(_worker, resource);
    }
}
