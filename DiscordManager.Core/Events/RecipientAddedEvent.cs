using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class RecipientAddedEvent : IEvent
{
    public SocketGroupUser GroupUser { get; set; }
}