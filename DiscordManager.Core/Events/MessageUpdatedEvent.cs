using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class MessageUpdatedEvent : IEvent
{
    public Cacheable<IMessage, ulong> OldMessage { get; set; }
    public SocketMessage NewMessage { get; set; }
    public ISocketMessageChannel Channel { get; set; }
}