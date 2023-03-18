using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ReactionAddedEvent : IEvent
{
    public Cacheable<IUserMessage, ulong> Message { get; set; }
    public Cacheable<IMessageChannel, ulong> Channel { get; set; }
    public SocketReaction Reaction { get; set; }
}