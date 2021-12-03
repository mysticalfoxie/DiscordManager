using DCM.Events.Discord;
using DCM.Events.Logging;
using DCM.Extensions;
using DCM.Interfaces;
using DCM.Models;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DCM
{
    public class DiscordManager : IDisposable
    {
        private readonly DependencyContainer _pluginDependencies = new(new ServiceCollection());
        private readonly List<FileInfo> _pluginLibraries = new();
        private readonly List<Type> _pluginTypes = new();
        private readonly List<Plugin> _plugins = new();
        private readonly IServiceProvider _provider;
        private readonly IEventMapper _eventMapper;
        private readonly Discord _discord;
        private CommandConfiguration _commandConfig = new();
        private Task _discordTask = Task.CompletedTask;
        private LoginCredentials _credentials;
        private CancellationToken _token;

        public IEventAggregator EventAggregator { get; } = new EventAggregator();
        public IDiscordClient Client { get; private set; }

        public DiscordManager()
        {
            _provider = new ServiceCollection()
                .AddSingleton<IEventMapper, EventMapper>()
                .AddSingleton<IEventAggregator>(prov => EventAggregator)
                .AddSingleton<IPluginManager, PluginManager>()
                .AddSingleton<ICommandManager, CommandManager>()
                .AddScoped<CommandConfiguration>(prov => _commandConfig)
                .AddScoped<IList<Plugin>>(prov => _plugins)
                .AddScoped<IList<Type>>(prov => _pluginTypes)
                .AddScoped<IList<FileInfo>>(prov => _pluginLibraries)
                .AddScoped<DependencyContainer>(prov => _pluginDependencies)
                .AddSingleton<DiscordManager>(this)
                .AddSingleton<AssemblyLoader>()
                .AddSingleton<Discord>()
                .BuildServiceProvider();

            _eventMapper = _provider.GetService<IEventMapper>();
            _discord = _provider.GetService<Discord>();
        }

        public DiscordManager ConfigureCommands(Func<CommandConfigurationBuilder, CommandConfigurationBuilder> configureFunction)
        {
            var builder = new CommandConfigurationBuilder();
            configureFunction.Invoke(builder);
            _commandConfig = builder.Build();

            return this;
        }

        public DiscordManager WithCredentials(LoginCredentials credentials)
        {
            _credentials = credentials 
                ?? throw new ArgumentNullException(nameof(credentials));

            return this;
        }

        public DiscordManager WithCancellationToken(CancellationToken token)
        {
            _token = token == default 
                ? throw new ArgumentNullException(nameof(token)) 
                : token;

            return this;
        }

        public DiscordManager WithServices(IServiceCollection services)
        {
            _pluginDependencies.Services = services
                ?? throw new ArgumentNullException(nameof(services));

            return this;
        }

        public DiscordManager AddPlugin(Plugin plugin)
        {
            _plugins.Add(plugin
                ?? throw new ArgumentNullException(nameof(plugin)));

            return this;
        }
        public DiscordManager AddPlugin(FileInfo pluginFile)
        {
            _pluginLibraries.Add(pluginFile
                ?? throw new ArgumentNullException(nameof(pluginFile)));
            return this;
        }
        public DiscordManager AddPlugin(Type pluginType)
        {
            _pluginTypes.Add(pluginType
                ?? throw new ArgumentNullException(nameof(pluginType)));

            return this;
        }
        public DiscordManager AddPlugin<TPlugin>() where TPlugin : Plugin
        {
            _pluginTypes.Add(typeof(TPlugin));
            return this;
        }

        public DiscordManager AddPluginDirectory(DirectoryInfo directory)
            => AddPluginCollector(new(directory
                ?? throw new ArgumentNullException(nameof(directory))));

        public DiscordManager AddPluginCollector(AssemblyCollector pluginCollector)
        {
            _pluginLibraries.AddRange(pluginCollector?.Files
                ?? throw new ArgumentNullException(nameof(pluginCollector)));

            return this;
        }

        private async Task StartClient(LoginCredentials credentials)
        {
            var pluginManager = _provider.GetService<IPluginManager>();
            var commandManager = _provider.GetService<ICommandManager>();

            pluginManager.LoadAll();
            EventAggregator.Publish<Events.Logging.LogEvent>(new($"{pluginManager.PluginCount} plugins loaded."));

            commandManager.InstantiateHandlers();
            EventAggregator.Publish<Events.Logging.LogEvent>(new($"{commandManager.HandlersCount} command handlers loaded."));

            _eventMapper.MapAllEvents();

            commandManager.StartObserving();
            EventAggregator.Publish<Events.Logging.LogEvent>(new($"Command observer started."));

            await pluginManager.InvokeInitialize();
            EventAggregator.Publish<TraceEvent>(new("Executed all initialization methods in ."));

            await _discord.StartClient(credentials.LoginToken);
            EventAggregator.Publish<Events.Logging.LogEvent>(new("Discord client logged in."));

            await pluginManager.InvokeStart();
            EventAggregator.Publish<TraceEvent>(new("Successfully executed all start methods."));

            await Task.Delay(-1);
        }

        public DiscordManager Start()
        {
            if (_credentials is null) throw new InvalidOperationException($"Please add the login credentials with '{nameof(WithCredentials)}'.");
            if (_discordTask.Status == TaskStatus.Running) throw new InvalidOperationException("The Discord client is already running.");

            Func<LoginCredentials, Task> startMethod = StartClient;
            _discordTask = startMethod.StartHandled(_credentials, _token);
            Client = _discord.Client;

            return this;
        }

        /// <summary>
        /// <para>Starts the discord client on this thread and waits for it completion.</para>
        /// <para>The only possible completion is a <strong>failiure</strong> so keep in mind that the Task <strong>won't complete</strong>!</para>
        /// </summary>
        /// <returns>a <see cref="System.Threading.Tasks.Task"/> that never completes. It is reserved for Discord.</returns>
        public Task StartAndWait()
        {
            Start();
            return _discordTask;
        }

        /// <summary>
        /// <para>Waits for the client to start completely and resolves after it is fully initialized</para>
        /// </summary>
        /// <returns>a <see cref="System.Threading.Tasks.Task"/> that completes when the client is initialized.</returns>
        public Task<DiscordManager> StartAsync()
        {
            var tcs = new TaskCompletionSource<DiscordManager>();
            EventAggregator.Subscribe<ReadyEvent>((eventArgs, subscription) =>
            {
                subscription.Unsubscribe();
                tcs.SetResult(this);
            });
            Start();
           

            return tcs.Task;
        }

        public void Dispose()
        {
            if (_discordTask?.Status == TaskStatus.Running)
                _discordTask.Dispose();

            _discord?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
