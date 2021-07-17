using DCM.Events.Discord;
using DCM.Interfaces;
using DCM.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCM
{
    public interface ICommandHandler
    {

    }

    class CommandHandler : ICommandHandler
    {
        private readonly Discord _discord;
        private readonly IEventEmitter _eventEmitter;
        private readonly DiscordManager _dcm;

        public CommandHandler(Discord discord, IEventEmitter eventEmitter, DiscordManager dcm)
        {
            _discord = discord;
            _eventEmitter = eventEmitter;
            _dcm = dcm;

            _discord.Client.MessageReceived += OnMessageReceived;
        }

        private Task OnMessageReceived(SocketMessage message)
        {
            if (string.IsNullOrWhiteSpace(_dcm.CommandPrefix)) return Task.CompletedTask;
            if (message is null) return Task.CompletedTask;
            if (string.IsNullOrWhiteSpace(message.Content)) return Task.CompletedTask;
            if (!message.Content.StartsWith(_dcm.CommandPrefix)) return Task.CompletedTask;
            if (message.Author.IsBot == true) return Task.CompletedTask;

            var content = message.Content[_dcm.CommandPrefix.Length..];
            var parts = content.Split(" ");

            _eventEmitter.Emit(new CommandReceivedEvent(message, new()
            {
                Name = parts[0],
                Arguments = parts.Length > 1 ? parts[1..] : null
            }));

            return Task.CompletedTask;
        }
    }
}
