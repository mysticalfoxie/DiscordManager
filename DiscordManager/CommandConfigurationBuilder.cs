using DCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM
{
    public class CommandConfigurationBuilder
    {
        internal CommandConfigurationBuilder() { }

        internal string Prefix { get; set; }
        internal List<Command> Commands { get; set; } = new();
        internal bool? IgnoreCasingRule { get; set; }

        public CommandConfigurationBuilder UsePrefix(char prefix)
            => UsePrefix(prefix.ToString());
        public CommandConfigurationBuilder UsePrefix(string prefix)
        {
            Prefix = prefix;
            return this;
        }

        public CommandConfigurationBuilder Bind<TCommandHandler>(string command) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler));
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Permissions permissions) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissions, (CommandOptions)null);
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissionsBuilder(new()).Build(), (CommandOptions)null);
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, CommandOptions options) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), (Permissions)null, options);
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), (Permissions)null, optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissionsBuilder(new()).Build(), optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder, CommandOptions options) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissionsBuilder(new()).Build(), options);
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Permissions permissions, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissions, optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind<TCommandHandler>(string command, Permissions permissions, CommandOptions options) where TCommandHandler : CommandHandler
            => Bind(command, typeof(TCommandHandler), permissions, options);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler)
            => Bind(command, commandHandler, (Permissions)null, (CommandOptions)null);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder)
            => Bind(command, commandHandler, permissionsBuilder(new()).Build(), (CommandOptions)null);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Permissions permissions)
            => Bind(command, commandHandler, permissions, (CommandOptions)null);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder)
            => Bind(command, commandHandler, (Permissions)null, optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, CommandOptions options)
            => Bind(command, commandHandler, (Permissions)null, options);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Permissions permissions, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder)
            => Bind(command, commandHandler, permissions, optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder, CommandOptions options)
            => Bind(command, commandHandler, permissionsBuilder(new()).Build(), options);
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Func<PermissionsBuilder, PermissionsBuilder> permissionsBuilder, Func<CommandOptionsBuilder, CommandOptionsBuilder> optionsBuilder)
            => Bind(command, commandHandler, permissionsBuilder(new()).Build(), optionsBuilder(new()).Build());
        public CommandConfigurationBuilder Bind(string command, Type commandHandler, Permissions permissions, CommandOptions options)
        {
            if (commandHandler.BaseType != typeof(CommandHandler))
                throw new InvalidOperationException($"The command handler {commandHandler} is not implementing type {nameof(CommandHandler)}.");

            if (Commands.Any(x => IsCommand(x, command)))
                Commands.First(x => IsCommand(x, command))
                    .HandlerTypes
                    .Add(commandHandler);
            else
                Commands.Add(new()
                {
                    Name = command,
                    HandlerTypes = new List<Type>() { commandHandler }
                });

            return this;
        }

        public CommandConfigurationBuilder IgnoreCasing(bool ignoreCasing = true)
        {
            IgnoreCasingRule = ignoreCasing;
            return this;
        }

        internal CommandConfiguration Build()
            => new()
            {
                Prefix = Prefix,
                Commands = Commands.ToArray(),
                IgnoreCasing = IgnoreCasingRule
            };

        bool IsCommand(Command command, string commandName)
            => IgnoreCasingRule ?? false
                ? command.Name.ToLower() == commandName.ToLower()
                : command.Name == commandName;
    }
}
