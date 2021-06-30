using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ReactionsRemovedForEmoteEvent : IEvent
    {
        public ReactionsRemovedForEmoteEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, IEmote emote)
        {
            Message = message;
            Channel = channel;
            Emote = emote;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public ISocketMessageChannel Channel { get; }
        public IEmote Emote { get; }
    }
}
