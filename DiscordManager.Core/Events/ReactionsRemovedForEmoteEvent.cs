using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class ReactionsRemovedForEmoteEvent : IEvent
{
    public Cacheable<IUserMessage, ulong> Message { get; set; }
    public Cacheable<IMessageChannel, ulong> Channel { get; set; }
    public IEmote Emote { get; set; }
}