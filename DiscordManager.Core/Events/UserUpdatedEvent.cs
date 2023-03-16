using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserUpdatedEvent : IEvent
{
    public UserUpdatedEvent(SocketUser oldUser, SocketUser newUser)
    {
        OldUser = oldUser;
        NewUser = newUser;
    }

    public SocketUser OldUser { get; }
    public SocketUser NewUser { get; }
}