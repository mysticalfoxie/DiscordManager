using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class RecipientRemovedEvent : IEvent
{
    public SocketGroupUser GroupUser { get; set; }
}