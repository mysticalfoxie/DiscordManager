using DCM.Models;
using System;
using System.Collections.Generic;

namespace DCM
{
    public class CommandCollection
    {
        private readonly List<Command> _commands = new();

        public int Count => _commands.Count;

        public CommandCollection AddCommand(Command command)
        {
            _commands.Add(command);
            return this;
        }
        public CommandCollection AddCommand(Action<CommandBuilder> configure)
        {
            var builder = new CommandBuilder();
            configure(builder);
            var command = builder.Build();
            _commands.Add(command);
            return this;
        }

        public CommandCollection RemoveCommand(Command command)
        {
            _commands.Remove(command);
            return this;
        }

        public CommandCollection Clear()
        {
            _commands.Clear();
            return this;
        }
    }
}
