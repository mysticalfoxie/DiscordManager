using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core;

public abstract class PluginBase
{
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    // public TService Get<TService>()
    // public TService Use<TService>()
    // [Injectable]

    public abstract void PreStart();
    public abstract void PostStart();
    public abstract Task PreStartAsync();
    public abstract Task PostStartAsync();
}

public abstract class PluginDependency
{
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    // public TService Get<TService>()
}