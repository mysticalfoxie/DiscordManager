using DCM.Models;
using System;

namespace DCM
{
    public class CommandBuilder
    {
        private readonly Command _command = new();

        public CommandBuilder WithName(string name)
        {
            _command.Name = name;
            return this;
        }

        public CommandBuilder Configure(Action<CommandOptionsBuilder> configure)
        {
            var optionsBuilder = new CommandOptionsBuilder();
            configure(optionsBuilder);
            _command.Options = optionsBuilder.Build();
            return this;
        }

        public CommandBuilder AddHandler<THandler>() where THandler : CommandHandler
        {
            _command.AddHandler<THandler>();
            return this;
        }
        public CommandBuilder AddHandler(Type handler) 
        {
            _command.AddHandler(handler);
            return this;
        }
        public CommandBuilder AddHandler(CommandHandler handler) 
        {
            _command.AddHandler(handler);
            return this;
        }

        public Command Build()
        {
            if (string.IsNullOrWhiteSpace(_command.Name))
                throw new InvalidOperationException("The command name is required to build a command.");

            return _command;
        }
    }
}
