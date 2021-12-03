using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class MessageDeletedEvent : Event
    {
        public MessageDeletedEvent(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
        {
            Message = message;
            Channel = channel;
        }

        public Cacheable<IMessage, ulong> Message { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
    }
}
