using DCM.Models;
using System;

namespace DCM
{
    public class HelpCommandBuilder
    {
        private readonly CommandBuilder _commandBuilder = new();
        private bool _handlerDefined = false;

        internal HelpCommandBuilder() { }

        public HelpCommandBuilder TriggerOn(string name)
        {
            _commandBuilder.WithName(name);
            return this;
        }

        public HelpCommandBuilder SetPermissions(Action<PermissionsBuilder> configure)
        {
            _commandBuilder.Configure(x => 
                x.WithPermissions(y => configure(y)));

            return this;
        }

        public HelpCommandBuilder SetHandler<THandler>() where THandler : CommandHandler
        {
            if (_handlerDefined)
                throw new InvalidOperationException("There already exists a command handler for this command.");

            _commandBuilder.AddHandler<THandler>();
            _handlerDefined = true;
            return this;
        }
        public HelpCommandBuilder SetHandler(Type handler)
        {
            if (_handlerDefined)
                throw new InvalidOperationException("There already exists a command handler for this command.");

            _commandBuilder.AddHandler(handler);
            _handlerDefined = true;
            return this;
        }
        public HelpCommandBuilder SetHandler(CommandHandler handler)
        {
            if (_handlerDefined)
                throw new InvalidOperationException("There already exists a command handler for this command.");

            _commandBuilder.AddHandler(handler);
            _handlerDefined = true;
            return this;
        }

        internal Command Build()
        {
            return _commandBuilder.Build();
        }
    }
}
