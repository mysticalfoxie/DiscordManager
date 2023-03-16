using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ReactionsRemovedForEmoteEvent : IEvent
{
    public ReactionsRemovedForEmoteEvent(Cacheable<IUserMessage, ulong> message,
        Cacheable<IMessageChannel, ulong> channel, IEmote emote)
    {
        Message = message;
        Channel = channel;
        Emote = emote;
    }

    public Cacheable<IUserMessage, ulong> Message { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
    public IEmote Emote { get; }
}