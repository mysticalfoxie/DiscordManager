using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System.Collections.Generic;

namespace DCM.Events.Discord
{
    public class MessagesBulkDeletedEvent : IEvent
    {
        public MessagesBulkDeletedEvent(IReadOnlyCollection<Cacheable<IMessage, ulong>> messages, ISocketMessageChannel channel)
        {
            Messages = messages;
            Channel = channel;
        }

        public IReadOnlyCollection<Cacheable<IMessage, ulong>> Messages { get; }
        public ISocketMessageChannel Channel { get; }
    }
}
