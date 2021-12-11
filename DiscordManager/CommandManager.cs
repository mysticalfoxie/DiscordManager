using DCM.Events.Discord;
using DCM.Events.Logging;
using DCM.Interfaces;
using DCM.Models;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DCM
{
    interface ICommandManager
    {
        int HandlersCount { get; }
        void InstantiateHandlers();
        void StartObserving();
    }

    class CommandManager : ICommandManager
    {
        private readonly DiscordManager _discordManager;
        private readonly DependencyContainer _dependencyContainer;
        private readonly IEventAggregator _eventAggregator;
        private readonly CommandConfiguration _commandConfig;

        public CommandManager(
            DiscordManager discordManager,
            DependencyContainer dependencyContainer,
            IEventAggregator eventAggregator,
            CommandConfiguration commandConfig)
        {
            _discordManager = discordManager;
            _dependencyContainer = dependencyContainer;
            _eventAggregator = eventAggregator;
            _commandConfig = commandConfig;
        }

        public int HandlersCount => _commandConfig.Commands
            .SelectMany(x => x.Handlers)
            .Count();

        public void InstantiateHandlers()
        {
            foreach (var command in _commandConfig.Commands)
                foreach (var handler in command.HandlerTypes)
                {
                    try
                    {
                        var instance = InstantiateHandler(handler);
                        command.Handlers.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        _eventAggregator.PublishAsync<ErrorEvent>(new(ex)).Wait();
                    }
                }
        }

        public void StartObserving()
        {
            _eventAggregator.Subscribe<MessageReceivedEvent>(Listener);
        }

        private void Listener(MessageReceivedEvent eventArgs)
        {
            if (string.IsNullOrWhiteSpace(eventArgs.Message.Content))
                return;

            var message = eventArgs.Message;
            if (!TryGetCommand(message, out var command))
                return;
            if (!IsAllowed(command, message))
                return;

            InvokeHandlers(command, message);
        }

        private bool TryGetCommand(SocketMessage message, out Command command)
        {
            var content = message.Content.Trim();
            var hasPrefix = content.StartsWith(_commandConfig.Prefix ?? "");
            var trueContent = hasPrefix ? content[(_commandConfig.Prefix?.Length ?? 0)..] : content;
            var parts = trueContent
                .Replace("\n\r", "\n")
                .Replace('\n', ' ')
                .Split(' ');

            var identifier = parts.Length > 0 ? parts[0] : null;
            command = _commandConfig.Commands
                // When prefix is needed and there is none provided, ignore this command
                .Where(x => !(x.Options?.RequiresPrefixOverride ?? true && !hasPrefix))
                .Where(x => !(x.Options?.Disabled ?? false))
                .FirstOrDefault(x => IsCommand(x, identifier));

            return command is not null;
        }

        private void InvokeHandlers(Command command, SocketMessage message)
        {
            var methods = JoinHandlersMethods(command.Handlers);
            foreach (var method in methods)
                Task.Factory.StartNew(async () =>
                {// Rethrow required, if not it would never be caught or even noticed.
                    try
                    {
                        await method.Invoke(message);
                    }
                    catch (Exception ex)
                    {
                        var handler = method.GetMethodInfo().DeclaringType;
                        await _eventAggregator.PublishAsync<ErrorEvent>(new(new($"An error occured in the command handler '{handler.FullName}'.", ex)));
                    }
                });
        }

        private static Func<SocketMessage, Task>[] JoinHandlersMethods(IEnumerable<CommandHandler> handlers)
        {
            var methods = new List<Func<SocketMessage, Task>>();

            methods.AddRange(handlers
                .Select<CommandHandler, Func<SocketMessage, Task>>(x => x.HandleAsync));

            // Wrap the synchronized Methods as a Task to prevent blocking the DCM Thread.
            methods.AddRange(handlers
                .Select<CommandHandler, Action<SocketMessage>>(x => x.Handle)
                .Select(x => new Func<SocketMessage, Task>(socketMessage =>
                    Task.Factory.StartNew(() =>
                    {
                        // Exceptions wouldn't be caught without this try-catch block
                        try
                        {
                            x.Invoke(socketMessage);
                        }
                        catch (Exception)
                        { 
                            throw;
                        }
                    })
                )));

            return methods.ToArray();
        }

        private bool IsCommand(Command command, string identifier)
        {
            var ignoreCasing = command.Options?.IgnoreCasingOverride 
                ?? _commandConfig.IgnoreCasing 
                ?? false;

            var hasAliases = (command.Options?.Aliases?.Length ?? 0) > 0;
            if (!hasAliases)
                return ignoreCasing
                    ? command.Name.ToLower() == identifier.ToLower()
                    : command.Name == identifier;

            var count = 1 + command.Options.Aliases.Length;
            var names = new string[count];
            names[0] = ignoreCasing
                ? command.Name.ToLower()
                : command.Name;

            for (int i = 1; i <= command.Options.Aliases.Length; i++)
                names[i] = ignoreCasing
                    ? command.Options.Aliases[i - 1].ToLower()
                    : command.Options.Aliases[i - 1];

            return names.Contains(ignoreCasing
                ? identifier.ToLower()
                : identifier);
        }

        private static bool IsAllowed(Command command, SocketMessage message)
            => command.Permissions?.Strategy switch
            {
                RestrictionStrategy.AllowOnlyPermitted => IsWhitelisted(command.Permissions, message),
                RestrictionStrategy.BlockAllUnpermitted => !IsBlacklisted(command.Permissions, message),
                null => true,
                _ => throw new NotSupportedException()
            };

        private static bool IsWhitelisted(Permissions permissions, SocketMessage message)
        {
            if (permissions.Restrictions.Users.Count > 0)
                if (!permissions.Restrictions.Users.Contains(message.Author.Id)) return false;

            if (permissions.Restrictions.Channels.Count > 0)
                if (!permissions.Restrictions.Channels.Contains(message.Channel.Id)) return false;

            if (permissions.Restrictions.Guilds.Count > 0)
            {
                if (message.Channel is not IGuildChannel guildChannel) return false;
                if (!permissions.Restrictions.Guilds.Contains(guildChannel.GuildId)) return false;
            }

            if (permissions.Restrictions.Roles.Count > 0)
            {
                if (message.Channel is not IGuildChannel guildChannel) return false;
                if (message.Author is not IGuildUser guildUser) return false;
                if (!guildUser.RoleIds.Any(x => permissions.Restrictions.Roles.Contains(x))) return false;
            }

            return true;
        }

        private static bool IsBlacklisted(Permissions permissions, SocketMessage message)
            => new bool[]
            {
                permissions.Restrictions.Roles.Any(x => message.Author is IGuildUser user && user.RoleIds.Contains(x)),
                permissions.Restrictions.Guilds.Any(x => message.Channel is IGuildChannel channel && x == channel.Guild.Id),
                permissions.Restrictions.Users.Contains(message.Author.Id),
                permissions.Restrictions.Channels.Contains(message.Channel.Id)
            }.Any(x => x);

        private CommandHandler InstantiateHandler(Type handler)
        {
            if (handler.BaseType != typeof(CommandHandler))
                throw new InvalidOperationException($"Cannot instantiate command handler '{handler.FullName}' because it has no base type of {nameof(CommandHandler)}");

            var constructors = handler.GetConstructors();
            if (constructors.Length > 1)
                throw new AmbiguousMatchException($"Cannot instantiate command handler '{handler.FullName}' because it has more than one constructor.");
            if (constructors.Length == 0)
                throw new InvalidOperationException($"Cannot instantiate command handler '{handler.FullName}' because it has no public constructor.");

            var ctor = constructors[0];
            var parameters = ctor.GetParameters();
            var instances = new object[parameters.Length];
            var services = _dependencyContainer.Services.BuildServiceProvider();
            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(DiscordManager))
                    instances[i] = _discordManager;
                else if (parameters[i].ParameterType == typeof(IEventAggregator))
                    instances[i] = _eventAggregator;
                else
                    instances[i] = services.GetService(parameters[i].ParameterType)
                        ?? throw new InvalidOperationException($"Cannot instantiate command handler '{handler.FullName}' because it includes the parameter '{parameters[i].ParameterType.FullName}' that could not be found in the service container.");
            }

            return (CommandHandler)ctor.Invoke(instances);
        }
    }
}
