using DCM.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

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

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public CommandBuilder Configure(Action<CommandBuilder> configure)
        {
            //configure // TODO: Continue here.. WTF.
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
        public CommandBuilder AddHandler(Action<SocketMessage> action)
        {
            _command.AddHandler(action);
            return this;
        }
        public CommandBuilder AddHandler(Func<SocketMessage, Task> func)
        {
            _command.AddHandler(func);
            return this;
        }
        public CommandBuilder AddHandler(CommandHandler handler) 
        {
            _command.AddHandler(handler);
            return this;
        }

        public CommandBuilder AddAlias(string alias)
        {
            _command.Aliases = _command.Aliases.Append(alias).ToArray();
            return this;
        }
        public CommandBuilder AddAliases(IEnumerable<string> aliases)
        {
            _command.Aliases = _command.Aliases.Concat(aliases).ToArray();
            return this;
        }

        public CommandBuilder IsDisabled(bool disabled = true)
        {
            _command.Disabled = disabled;
            return this;
        }

        public CommandBuilder WithNoPrefix()
        {
            _command.NoPrefix = true;
            return this;
        }

        public CommandBuilder OverridesIgnoreCasing(bool ignoreCasing = true)
        {
            _command.IgnoreCasingOverride = ignoreCasing;
            return this;
        }

        public CommandBuilder WithPermissions(Action<PermissionsBuilder> configure)
        {
            var permissionsBuilder = new PermissionsBuilder();
            configure(permissionsBuilder);
            _command.Permissions = permissionsBuilder.Build();
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
