using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class VoiceServerUpdatedEvent : IEvent
    {
        public VoiceServerUpdatedEvent(SocketVoiceServer voiceServer)
        {
            VoiceServer = voiceServer;
        }

        public SocketVoiceServer VoiceServer { get; }
    }
}
