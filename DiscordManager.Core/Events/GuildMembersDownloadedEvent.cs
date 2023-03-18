using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildMembersDownloadedEvent : IEvent
{
    public SocketGuild Guild { get; set; }
}