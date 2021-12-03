using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class VoiceServerUpdatedEvent : Event
    {
        public VoiceServerUpdatedEvent(SocketVoiceServer voiceServer)
        {
            VoiceServer = voiceServer;
        }

        public SocketVoiceServer VoiceServer { get; }
    }
}
