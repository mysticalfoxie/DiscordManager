using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildUnavailableEvent : IEvent
{
    public SocketGuild Guild { get; set; }
}