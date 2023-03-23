namespace DCM.Core;

public abstract class DCMPlugin : ServiceContainer
{
    public virtual void PostStart()
    {
    }

    public virtual Task PostStartAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void PreStart()
    {
    }

    public virtual Task PreStartAsync()
    {
        return Task.CompletedTask;
    }
}