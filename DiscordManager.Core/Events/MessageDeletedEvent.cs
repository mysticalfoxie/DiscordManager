using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class MessageDeletedEvent : IEvent
{
    public MessageDeletedEvent(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
    {
        Message = message;
        Channel = channel;
    }

    public Cacheable<IMessage, ulong> Message { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
}