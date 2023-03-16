using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class RecipientAddedEvent : IEvent
{
    public RecipientAddedEvent(SocketGroupUser user)
    {
        User = user;
    }

    public SocketGroupUser User { get; }
}