using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ThreadMemberLeftEvent : IEvent
{
    public ThreadMemberLeftEvent(SocketThreadUser user)
    {
        User = user;
    }

    public SocketThreadUser User { get; }
}