public class BuildBaseTask : Task
{
    private Base _base;
    
    public BuildBaseTask(TaskPriority priority, Base @base) : base(priority)
    {
        _base = @base;
    }

    public override bool TryRun()
    {
        if (_base.TryBuildNewBase() && _base.ResourceCounter.TryCost(5))
            return true;
        
        return false;
    }
}
