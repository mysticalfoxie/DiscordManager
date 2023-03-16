using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DCM;

public abstract class CommandHandler
{
    private readonly Action<SocketMessage> _action;
    private readonly Func<SocketMessage, Task> _func;

    public CommandHandler()
    {
    }

    public CommandHandler(Action<SocketMessage> action)
    {
        _action = action;
    }

    public CommandHandler(Func<SocketMessage, Task> func)
    {
        _func = func;
    }

    public IDiscordClient DiscordClient { get; internal set; }
    public IEventAggregator EventAggregator { get; internal set; }

    public virtual void Handle(SocketMessage message)
    {
        if (_action != default)
            _action(obj: message);
    }

    public virtual async Task HandleAsync(SocketMessage message)
    {
        if (_func != default)
            await _func(arg: message);
    }
}