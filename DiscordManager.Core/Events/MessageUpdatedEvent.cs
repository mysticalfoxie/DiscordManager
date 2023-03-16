using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class MessageUpdatedEvent : IEvent
{
    public MessageUpdatedEvent(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage,
        ISocketMessageChannel channel)
    {
        OldMessage = oldMessage;
        NewMessage = newMessage;
        Channel = channel;
    }

    public Cacheable<IMessage, ulong> OldMessage { get; }
    public SocketMessage NewMessage { get; }
    public ISocketMessageChannel Channel { get; }
}