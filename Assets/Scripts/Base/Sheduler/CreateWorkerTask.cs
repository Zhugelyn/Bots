public class CreateWorkerTask : Task
{
    private Base _base;
    
    public CreateWorkerTask(TaskPriority priority, Base @base) : base(priority)
    {
        _base = @base;
    }

    public override bool TryExecute()
    {
        if (_base.ResourceCounter.TryCost(3) && _base.TryCreateWorker())
            return true;
        
        return false;
    }
}
