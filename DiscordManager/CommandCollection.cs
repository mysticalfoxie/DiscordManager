using DCM.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DCM
{
    public interface ICommandCollection : IEnumerable<Command>
    {
        int Count { get; }

        ICommandCollection AddCommand(Action<CommandBuilder> configure);
        ICommandCollection AddCommand(Command command);
        ICommandCollection Clear();
        ICommandCollection RemoveCommand(Command command);
    }

    class CommandCollection : ICommandCollection
    {
        private readonly List<Command> _commands = new();

        public int Count => _commands.Count;

        public ICommandCollection AddCommand(Command command)
        {
            _commands.Add(command);
            return this;
        }
        public ICommandCollection AddCommand(Action<CommandBuilder> configure)
        {
            var builder = new CommandBuilder();
            configure(builder);
            var command = builder.Build();
            _commands.Add(command);
            return this;
        }

        public ICommandCollection RemoveCommand(Command command)
        {
            _commands.Remove(command);
            return this;
        }

        public ICommandCollection Clear()
        {
            _commands.Clear();
            return this;
        }

        public IEnumerator<Command> GetEnumerator()
            => _commands.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
