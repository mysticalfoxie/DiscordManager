using DCM.Interfaces;
using Discord;
using System.Collections.Generic;

namespace DCM.Events.Discord
{
    public class MessagesBulkDeletedEvent : IEvent
    {
        public MessagesBulkDeletedEvent(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages, Cacheable<IMessageChannel, ulong> channel)
        {
            Messages = messages;
            Channel = channel;
        }

        public IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
    }
}
