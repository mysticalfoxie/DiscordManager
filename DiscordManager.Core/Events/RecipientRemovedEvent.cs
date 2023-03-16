using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class RecipientRemovedEvent : IEvent
{
    public RecipientRemovedEvent(SocketGroupUser user)
    {
        User = user;
    }

    public SocketGroupUser User { get; }
}