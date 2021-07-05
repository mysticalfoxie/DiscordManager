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
        private Task _discordTask = Task.CompletedTask;
        private LoginCredentials _credentials;
        private CancellationToken _token;

        internal static IDiscordClient ActiveClient { get; set; }
        public IReadOnlyList<Plugin> Plugins => _plugins;
        public IServiceProvider InjectableServices { get; set; }
        public IEventEmitter EventEmitter { get; } = new EventEmitter();

        public DiscordManager()
        {
            _provider = new ServiceCollection()
                .AddSingleton<IEventMapper, EventMapper>()
                .AddSingleton<IEventEmitter>(prov => EventEmitter)
                .AddSingleton<IPluginManager, PluginManager>()
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

        public DiscordManager WithCredentials(LoginCredentials credentials)
        {
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            return this;
        }

        public DiscordManager WithCancellationToken(CancellationToken token)
        {
            _token = token != default ? token : throw new ArgumentNullException(nameof(token));
            return this;
        }

        public DiscordManager WithServices(IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            _pluginDependencies.Services = services;

            return this;
        }

        public DiscordManager AddPluginCollector(AssemblyCollector pluginCollector)
        {
            _pluginLibraries.AddRange(pluginCollector.Files);
            return this;
        }

        public DiscordManager AddPlugin(Plugin plugin)
        {
            _plugins.Add(plugin);
            return this;
        }

        public DiscordManager AddPlugin(FileInfo pluginLibrary)
        {
            _pluginLibraries.Add(pluginLibrary);
            return this;
        }

        public DiscordManager AddPlugin(Type pluginType)
        {
            _pluginTypes.Add(pluginType);
            return this;
        }

        private async Task StartClient(LoginCredentials credentials)
        {
            var pluginManager = _provider.GetService<IPluginManager>();

            pluginManager.LoadAll();
            EventEmitter.Emit<LogEvent>(new("All plugins loaded."));

            _eventMapper.MapAllEvents();

            await pluginManager.InvokeInitialize();
            EventEmitter.Emit<TraceEvent>(new("Successfully executed all initialization methods."));

            await _discord.StartClient(credentials.LoginToken);
            EventEmitter.Emit<LogEvent>(new("Discord client ready."));

            await pluginManager.InvokeStart();
            EventEmitter.Emit<TraceEvent>(new("Successfully executed all start methods."));

            await Task.Delay(-1);
        }

        public void Start()
        {
            if (_credentials is null) throw new InvalidOperationException($"Please add the login credentials with '{nameof(WithCredentials)}'.");
            if (_discordTask.Status == TaskStatus.Running) throw new InvalidOperationException("The Discord client is already running.");

            Func<LoginCredentials, Task> startMethod = StartClient;
            _discordTask = startMethod.StartHandled(_credentials, _token);
        }

        /// <summary>
        /// Starts the discord client on this thread. It never completes!
        /// </summary>
        /// <returns>a Task that never completes. It is reserved for Discord.</returns>
        public Task StartAsync()
        {
            Start();
            return _discordTask;
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
