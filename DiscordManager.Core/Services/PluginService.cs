using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DCM.Exceptions;
using DCM.Extensions;
using Discord;
using DiscordManager.Core.Events.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DCM;

internal interface IPluginManager
{
    int PluginCount { get; }
    void LoadAll();
    Task InvokeInitialize();
    Task InvokeStart();
}

internal class PluginManager : IPluginManager
{
    private readonly AssemblyLoader _assemblyLoader;
    private readonly DependencyContainer _dependencies;
    private readonly Discord _discord;
    private readonly IEventAggregator _eventAggregator;
    private readonly IList<FileInfo> _pluginLibraries;
    private readonly IList<Plugin> _plugins;
    private readonly IList<Type> _pluginTypes;

    public PluginManager(
        IList<FileInfo> pluginLibraries,
        IList<Type> pluginTypes,
        IList<Plugin> plugins,
        DependencyContainer dependencies,
        IEventAggregator eventEmitter,
        AssemblyLoader assemblyLoader,
        Discord discord)
    {
        _assemblyLoader = assemblyLoader;
        _pluginLibraries = pluginLibraries;
        _pluginTypes = pluginTypes;
        _plugins = plugins;
        _dependencies = dependencies;
        _eventAggregator = eventEmitter;
        _discord = discord;
        _assemblyLoader.AssemblyLoad += AssemblyLoader_AssemblyLoad;
        _assemblyLoader.Error += AssemblyLoader_Error;

        _dependencies.Services.AddSingleton<IDiscordClient>(prov => _discord.Client);
        _dependencies.Services.AddSingleton(prov => _eventAggregator);
        _dependencies.Services.AddScoped<IDCMContext, DCMContext>(prov =>
            new DCMContext(client: _discord.Client, events: _eventAggregator, services: _dependencies));
    }

    public int PluginCount => _plugins.Count;

    public void LoadAll()
    {
        // Locating
        var libraries = _pluginLibraries.ToArray();
        var types = _pluginTypes.ToArray();

        // Extraction
        var assemblies = _assemblyLoader.LoadAll(assemblyFiles: libraries);
        var pluginTypes = assemblies
            .SelectMany(assembly => ExtractPluginTypes(assembly: assembly))
            .Concat(second: types)
            .ToArray();

        // Loading
        var provider = RegisterPlugins(pluginTypes: pluginTypes);
        foreach (var plugin in InstantiatePlugins(pluginTypes: pluginTypes, provider: provider))
        {
            plugin.DiscordClient = _discord.Client;
            plugin.EventAggregator = _eventAggregator;
            _plugins.Add(item: plugin);
        }
    }

    public async Task InvokeInitialize()
    {
        foreach (var plugin in _plugins)
        {
            ExecuteMethodHandled(plugin: plugin, nameof(Plugin.Initialize));
            await ExecuteMethodHandledAsync(plugin: plugin, nameof(Plugin.InitializeAsync));
        }
    }

    public async Task InvokeStart()
    {
        foreach (var plugin in _plugins)
        {
            ExecuteMethodHandled(plugin: plugin, nameof(Plugin.Start));
            await ExecuteMethodHandledAsync(plugin: plugin, nameof(Plugin.StartAsync));
        }
    }

    private IEnumerable<Plugin> InstantiatePlugins(IEnumerable<Type> pluginTypes, IServiceProvider provider)
    {
        foreach (var pluginType in pluginTypes)
        {
            Plugin plugin;
            try
            {
                plugin = (Plugin)provider.GetRequiredService(serviceType: pluginType);
            }
            catch (Exception ex)
            {
                var error = new PluginException(
                    $"An error occured when trying to instantiate the plugin '{pluginType.FullName}'.", inner: ex);
                _eventAggregator.PublishAsync<ErrorEvent>(eventArgs: error).Wait();
                continue;
            }

            yield return plugin;
        }
    }

    private IServiceProvider RegisterPlugins(IEnumerable<Type> pluginTypes)
    {
        foreach (var pluginType in pluginTypes)
            _dependencies.Services.AddSingleton(serviceType: pluginType);

        return _dependencies.Services.BuildServiceProvider();
    }


    private void AssemblyLoader_AssemblyLoad(Assembly assembly)
    {
        _eventAggregator?.Publish<TraceEvent>(
            $"Plugin '{assembly.GetName().Name}' loaded at version '{string.Join('.', assembly.GetName().Version.ToString().Split('.').Take(3))}'.");
    }

    private void AssemblyLoader_Error(Exception error)
    {
        _eventAggregator?.Publish<ErrorEvent>(eventArgs: error);
    }

    private void ExecuteMethodHandled(Plugin plugin, string methodName, object[] parameters = null)
    {
        try
        {
            plugin.InvokeMethod(methodName: methodName, parameters: parameters);
        }
        catch (Exception ex)
        {
            _eventAggregator.PublishAsync<ErrorEvent>(new ErrorEvent(new PluginException(
                $"An error occured in the plugin '{plugin.GetType().Name}' when invoking the method '{methodName}'.",
                inner: ex))).Wait();
            plugin.IsRunning = false;
        }
    }

    private async Task ExecuteMethodHandledAsync(Plugin plugin, string methodName, object[] parameters = null)
    {
        try
        {
            await plugin.InvokeMethodAsync(methodName: methodName, parameters: parameters);
        }
        catch (Exception ex)
        {
            await _eventAggregator.PublishAsync<ErrorEvent>(new ErrorEvent(new PluginException(
                $"An error occured in the plugin '{plugin.GetType().Name}' when invoking the method '{methodName}'.",
                inner: ex)));
            plugin.IsRunning = false;
        }
    }

    private static IEnumerable<Type> ExtractPluginTypes(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(type => type.IsClass)
            .Where(@class => @class.IsPublic)
            .Where(@class => !@class.IsAbstract)
            .Where(@class => @class.BaseType == typeof(Plugin));
    }
}