using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class MessagesBulkDeletedEvent : IEvent
{
    public IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages { get; set; }
    public Cacheable<IMessageChannel, ulong> Channel { get; set; }
}