using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ChannelCreatedEvent : IEvent
{
    public SocketChannel Channel { get; set; }
}