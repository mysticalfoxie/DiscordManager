using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DCM.Extensions;
using DCM.Models;
using Discord;
using DiscordManager.Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace DCM;

public class DiscordManager : IDisposable
{
    private readonly ConfigLoader _configLoader;
    private readonly Discord _discord;
    private readonly IEventMapper _eventMapper;
    private readonly DependencyContainer _pluginDependencies = new(new ServiceCollection());
    private readonly List<FileInfo> _pluginLibraries = new();
    private readonly List<Plugin> _plugins = new();
    private readonly List<Type> _pluginTypes = new();
    private readonly IServiceProvider _provider;
    private LoginCredentials _credentials;
    private Task _discordTask = Task.CompletedTask;
    private CancellationToken _token;

    public DiscordManager()
    {
        _provider = new ServiceCollection()
            .AddSingleton<IEventMapper, EventMapper>()
            .AddSingleton(prov => EventAggregator)
            .AddSingleton<IPluginManager, PluginManager>()
            .AddScoped<IList<Plugin>>(prov => _plugins)
            .AddScoped<IList<Type>>(prov => _pluginTypes)
            .AddScoped<IList<FileInfo>>(prov => _pluginLibraries)
            .AddScoped(prov => _pluginDependencies)
            .AddSingleton(this)
            .AddSingleton<AssemblyLoader>()
            .AddSingleton<ConfigLoader>()
            .AddSingleton<Discord>()
            .BuildServiceProvider();

        _eventMapper = _provider.GetService<IEventMapper>();
        _discord = _provider.GetService<Discord>();
        _configLoader = _provider.GetService<ConfigLoader>();
    }

    public IEventAggregator EventAggregator { get; } = new EventAggregator();
    public IDiscordClient Client { get; private set; }

    public void Dispose()
    {
        if (_discordTask?.Status == TaskStatus.Running)
            _discordTask.Dispose();

        _discord?.Dispose();
        GC.SuppressFinalize(this);
    }

    public DiscordManager ConfigureCommandManager(Action<CommandConfigurationBuilder> configureFunction)
    {
        var builder = new CommandConfigurationBuilder(instance: _commandConfiguration);
        configureFunction.Invoke(obj: builder);
        builder.Build();

        return this;
    }

    public DiscordManager CommandCollection(Action<ICommandCollection> configure)
    {
        configure(obj: _commandCollection);
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

    public DiscordManager UseConfig<T>(string filename) where T : class
    {
        if (string.IsNullOrWhiteSpace(value: filename))
            throw new ArgumentNullException(nameof(filename));

        _configLoader.AddConfigFile<T>(filename: filename);

        return this;
    }

    public DiscordManager UseConfig<T>(string filename, Action<ConfigDescriptor<T>> configure) where T : class
    {
        UseConfig<T>(filename: filename);

        var config = _configLoader.ReadConfig<T>();
        var descriptor = new ConfigDescriptor<T>(config: config);

        configure(obj: descriptor);

        descriptor.LoginToken

        return this;
    }

    public DiscordManager WithServices(IServiceCollection services)
    {
        _pluginDependencies.Services = services
                                       ?? throw new ArgumentNullException(nameof(services));

        services.AddSingleton(this);
        services.AddSingleton(implementationInstance: EventAggregator);

        return this;
    }

    public DiscordManager Configure(Action<IDiscordConfigBuilder> configure)
    {
        var builder = new DiscordConfigBuilder(config: _discord.Config);
        configure(obj: builder);
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
    {
        return AddPluginCollector(new AssemblyCollector(directory
                                                        ?? throw new ArgumentNullException(nameof(directory))));
    }

    public DiscordManager AddPluginCollector(AssemblyCollector pluginCollector)
    {
        _pluginLibraries.AddRange(pluginCollector?.Files
                                  ?? throw new ArgumentNullException(nameof(pluginCollector)));

        return this;
    }

    private async Task StartClient(LoginCredentials credentials)
    {
        var pluginManager = _provider.GetService<IPluginManager>();
        // var commandManager = _provider.GetService<ICommandManager>();

        pluginManager.LoadAll();
        await EventAggregator.PublishAsync<InfoEvent>($"{pluginManager.PluginCount} plugins loaded.");

        // commandManager.InstantiateHandlers();
        // await EventAggregator.PublishAsync<InfoEvent>($"{commandManager.HandlersCount} command handlers loaded."));

        _eventMapper.MapAllEvents();

        // commandManager.StartObserving();
        await EventAggregator.PublishAsync<InfoEvent>("Command observer started.");

        await pluginManager.InvokeInitialize();
        await EventAggregator.PublishAsync<TraceEvent>("Executed all initialization methods.");

        await _discord.StartClient(loginToken: credentials.LoginToken);
        await EventAggregator.PublishAsync<InfoEvent>("Discord client logged in.");

        await pluginManager.InvokeStart();
        await EventAggregator.PublishAsync<TraceEvent>("Successfully executed all start methods.");

        await Task.Delay(-1);
    }

    public DiscordManager Start()
    {
        if (_credentials is null)
            throw new InvalidOperationException($"Please add the login credentials with '{nameof(WithCredentials)}'.");
        if (_discordTask.Status == TaskStatus.Running)
            throw new InvalidOperationException("The Discord client is already running.");

        var startMethod = StartClient;
        _discordTask = startMethod.StartHandled(parameter: _credentials, token: _token);
        Client = _discord.Client;

        return this;
    }

    /// <summary>
    ///     <para>Starts the discord client on this thread and waits for it completion.</para>
    ///     <para>
    ///         The only possible completion is a <strong>failiure</strong> so keep in mind that the Task
    ///         <strong>won't complete</strong>!
    ///     </para>
    /// </summary>
    /// <returns>a <see cref="System.Threading.Tasks.Task" /> that never completes. It is reserved for Discord.</returns>
    public Task StartAndWait()
    {
        Start();
        return _discordTask;
    }

    /// <summary>
    ///     <para>Waits for the client to start completely and resolves after it is fully initialized</para>
    /// </summary>
    /// <returns>a <see cref="System.Threading.Tasks.Task" /> that completes when the client is initialized.</returns>
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
}