using DCM.Interfaces;
using DCM.Models;
using Discord;

namespace DCM.Events.Discord
{
    public class CommandReceivedEvent : IEvent
    {
        public CommandReceivedEvent(IMessage message, Command command)
        {
            Message = message;
            Command = command;
        }

        public IMessage Message { get; }
        public Command Command { get; }
    }
}
