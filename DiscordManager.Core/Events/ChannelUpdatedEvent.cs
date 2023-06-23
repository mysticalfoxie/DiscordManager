using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ChannelUpdatedEvent : IEvent
{
    public SocketChannel OldChannel { get; set; }
    public SocketChannel NewChannel { get; set; }
}