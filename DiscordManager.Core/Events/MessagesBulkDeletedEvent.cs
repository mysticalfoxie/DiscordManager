using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class MessagesBulkDeletedEvent : IEvent
{
    public MessagesBulkDeletedEvent(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages,
        Cacheable<IMessageChannel, ulong> channel)
    {
        Messages = messages;
        Channel = channel;
    }

    public IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
}