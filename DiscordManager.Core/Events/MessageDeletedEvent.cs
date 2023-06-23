using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class MessageDeletedEvent : IEvent
{
    public Cacheable<IMessage, ulong> Message { get; set; }
    public Cacheable<IMessageChannel, ulong> Channel { get; set; }
}