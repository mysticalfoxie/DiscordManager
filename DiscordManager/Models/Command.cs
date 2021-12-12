using System;
using System.Collections.Generic;

namespace DCM.Models
{
    public class Command
    {
        private readonly List<CommandHandler> _handlers = new();
        private readonly List<Type> _types = new();
        public string Name { get; set; }
        public CommandOptions Options { get; set; }
        public Permissions Permissions { get; set; }
        public int HandlerCount => _handlers.Count;

        public Command AddHandler<THandler>() where THandler : CommandHandler
        {
            _types.Add(typeof(THandler));
            return this;
        }
        public Command AddHandler(Type handler)
        {
            _types.Add(handler);
            return this;
        }
        public Command AddHandler(CommandHandler handler)
        {
            _handlers.Add(handler);
            return this;
        }

        public Command RemoveHandler<THandler>() where THandler : CommandHandler
        {
            _handlers.RemoveAll(handler => handler.GetType() == typeof(THandler));
            return this;
        }
        public Command RemoveHandler(Type handlerType)
        {
            _handlers.RemoveAll(handler => handler.GetType() == handlerType);
            return this;
        }
        public Command RemoveHandler(CommandHandler handler)
        {
            _handlers.Remove(handler);
            return this;
        }
    }
}
