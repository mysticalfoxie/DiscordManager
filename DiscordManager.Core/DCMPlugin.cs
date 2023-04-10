using DCM.Core.Models;

namespace DCM.Core;

public abstract class DCMPlugin : DiscordContainer
{
    public DCMPluginConfig PluginConfig { get; internal set; }

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