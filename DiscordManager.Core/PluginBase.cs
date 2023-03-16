using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core;

public abstract class PluginBase
{
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    public abstract Task InitializeAsync();
    public abstract void Initialize();
    public abstract void Start();
    public abstract Task StartAsync();
}