using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCM.Events.Discord
{
    public class ReactionRemovedEvent : IEvent
    {
        public ReactionRemovedEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            Message = message;
            Channel = channel;
            Reaction = reaction;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public ISocketMessageChannel Channel { get; }
        public SocketReaction Reaction { get; }
    }
}
