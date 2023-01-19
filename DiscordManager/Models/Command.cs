using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCM.Models
{
    public class Command
    {
        private readonly List<CommandHandler> _handler = new();
        public IReadOnlyCollection<CommandHandler> Handler => _handler;
        public string Name { get; set; }
        public Permissions Permissions { get; set; }
        public string[] Aliases { get; set; }
        public bool Disabled { get; set; }
        public bool NoPrefix { get; set; }
        public bool? IgnoreCasingOverride { get; set; }

        public CommandHandler AddHandler<THandler>() where THandler : CommandHandler
        {
            var handler = CommandManager.InstantiateHandler(typeof(THandler));
            _handler.Add(handler);
            return handler;
        }
        public CommandHandler AddHandler(Type handlerType)
        {
            var handler = CommandManager.InstantiateHandler(handlerType);
            _handler.Add(handler);
            return handler;
        }
        public CommandHandler AddHandler(CommandHandler handler)
        {
            _handler.Add(handler);
            return handler;
        }
        public CommandHandler AddHandler(Action<SocketMessage> action)
            => AddHandler(action);
        public CommandHandler AddHandler(Func<SocketMessage, Task> func)
            => AddHandler(func);

        public void RemoveHandler<THandler>() where THandler : CommandHandler
            => _handler.RemoveAll(handler => handler.GetType() == typeof(THandler));
        public void RemoveHandler(Type handlerType)
            => _handler.RemoveAll(handler => handler.GetType() == handlerType);
        public void RemoveHandler(CommandHandler handler)
            => _handler.Remove(handler);
    }
}
