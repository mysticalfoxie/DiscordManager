using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ThreadMemberJoinedEvent : IEvent
{
    public ThreadMemberJoinedEvent(SocketThreadUser user)
    {
        User = user;
    }

    public SocketThreadUser User { get; }
}