﻿using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class SpeakerAddedEvent : Event
    {
        public SpeakerAddedEvent(SocketStageChannel channel, SocketGuildUser user)
        {
            Channel = channel;
            User = user;
        }

        public SocketStageChannel Channel { get; }
        public SocketGuildUser User { get; }
    }
}
