using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class WebhooksUpdatedEvent : IEvent
{
    public SocketGuild Guild { get; set; }
    public SocketChannel Channel { get; set; }
}