using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class MessageReceivedEvent : IEvent
{
    public MessageReceivedEvent(SocketMessage message)
    {
        Message = message;
    }

    public SocketMessage Message { get; }
}