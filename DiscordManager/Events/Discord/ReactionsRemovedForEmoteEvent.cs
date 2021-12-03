using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class ReactionsRemovedForEmoteEvent : Event
    {
        public ReactionsRemovedForEmoteEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, IEmote emote)
        {
            Message = message;
            Channel = channel;
            Emote = emote;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
        public IEmote Emote { get; }
    }
}
