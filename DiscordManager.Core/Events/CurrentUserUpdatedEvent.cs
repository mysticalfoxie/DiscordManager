using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class CurrentUserUpdatedEvent : IEvent
{
    public CurrentUserUpdatedEvent(SocketSelfUser oldUser, SocketSelfUser newUser)
    {
        OldUser = oldUser;
        NewUser = newUser;
    }

    public SocketSelfUser OldUser { get; }
    public SocketSelfUser NewUser { get; }
}