using DCM.Interfaces;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCM.Events.Discord
{
    public class RecipientAddedEvent : Event
    {
        public RecipientAddedEvent(SocketGroupUser user)
        {
            User = user;
        }

        public SocketGroupUser User { get; }
    }
}
