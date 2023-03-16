using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class VoiceServerUpdatedEvent : IEvent
{
    public VoiceServerUpdatedEvent(SocketVoiceServer voiceServer)
    {
        VoiceServer = voiceServer;
    }

    public SocketVoiceServer VoiceServer { get; }
}