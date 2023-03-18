using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core;

public abstract class PluginBase
{
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    // public TService Get<TService>()
    // public TService Use<TService>()
    // [Injectable]

    public virtual void PreStart()
    {
    }

    public virtual void PostStart()
    {
    }

    public virtual Task PreStartAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task PostStartAsync()
    {
        return Task.CompletedTask;
    }
}

public abstract class PluginDependency
{
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    // public TService Get<TService>()
}