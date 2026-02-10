public class BuildBaseTask : Task
{
    private Base _base;
    
    public BuildBaseTask(TaskPriority priority, Base @base) : base(priority)
    {
        _base = @base;
    }

    public override bool TryRun()
    {
        if (_base.ResourceCounter.TryCost(5) == false)
            return false;
        
        if (_base.TryBuildNewBase())
            return true;
        
        _base.ResourceCounter.Refund(5);
        return false;
    }
}
