using DCM.Core.Enums;
using DCM.Core.Interfaces;
using DCM.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace DCM.Core.Services;

public class PluginService : IPluginService
{
    private readonly IAssemblyService _assemblyService;
    private readonly IDependencyService _dependencyService;
    private readonly IDiscordService _discordService;
    private readonly IEventService _eventService;
    private readonly ILogger<PluginService> _logger;

    public PluginService(
        IDependencyService dependencyService,
        IEventService eventService,
        IDiscordService discordService,
        ILogger<PluginService> logger,
        IAssemblyService assemblyService)
    {
        _dependencyService = dependencyService;
        _eventService = eventService;
        _discordService = discordService;
        _logger = logger;
        _assemblyService = assemblyService;
    }

    public List<FileInfo> PluginFiles { get; } = new();
    public List<DCMPlugin> PluginInstances { get; } = new();
    public List<Type> PluginTypes { get; } = new();

    public void Invoke(PluginInvokationTarget target)
    {
        var names = GetMethodNames(target: target);
        foreach (var plugin in PluginInstances)
        {
            InvokeSynchronousMethod(instance: plugin, methodName: names.SyncMethodName);
            InvokeAsynchronousMethod(instance: plugin, methodName: names.AsyncMethodName);
        }
    }

    public int Load()
    {
        LoadPluginTypesFromAssemblies();
        InstantiatePlugins();

        return PluginInstances.Count;
    }


    private static PluginMethodNames GetMethodNames(PluginInvokationTarget target)
    {
        return target switch
        {
            PluginInvokationTarget.PostStart => new PluginMethodNames(
                nameof(DCMPlugin.PostStart),
                nameof(DCMPlugin.PostStartAsync)),
            PluginInvokationTarget.PreStart => new PluginMethodNames(
                nameof(DCMPlugin.PreStart),
                nameof(DCMPlugin.PreStartAsync)),
            _ => throw new NotSupportedException()
        };
    }

    private DCMPlugin InstantiatePlugin(Type type)
    {
        try
        {
            var pluginServices = new ServiceCollection();
            var injectables = _dependencyService.SearchInjectables(assembly: type.Assembly);
            pluginServices.Add(descriptors: injectables);

            var plugin = (DCMPlugin)_dependencyService.CreateInstance(type: type, secondary: pluginServices);

            return PropagatePlugin(plugin: plugin, services: pluginServices);
        }
        catch (Exception ex)
        {
            _logger.Log(logLevel: LogLevel.Error, exception: ex,
                $"An error occured instantiating a plugin of type '{type.FullName}'.");
            return null;
        }
    }

    private void InstantiatePlugins()
    {
        var plugins = PluginTypes
            .Select(selector: InstantiatePlugin)
            .Where(x => x is not null);
        PluginInstances.AddRange(collection: plugins);
    }

    private void InvokeAsynchronousMethod(object instance, string methodName)
    {
        Task.Factory.StartNew(async () =>
        {
            try
            {
                var method = instance.GetType().GetMethod(name: methodName);
                var task = (Task)method!.Invoke(obj: instance, Array.Empty<object>())!;
                await task;
            }
            catch (Exception ex)
            {
                _logger.Log(logLevel: LogLevel.Error, exception: ex,
                    $"An error occured invoking the plugins method '{methodName}'.");
            }
        });
    }

    private void InvokeSynchronousMethod(object instance, string methodName)
    {
        Task.Factory.StartNew(() =>
        {
            try
            {
                var method = instance.GetType().GetMethod(name: methodName);
                method!.Invoke(obj: instance, Array.Empty<object>());
            }
            catch (Exception ex)
            {
                _logger.Log(logLevel: LogLevel.Error, exception: ex,
                    $"An error occured invoking the plugins method '{methodName}'.");
            }
        });
    }

    private void LoadPluginTypesFromAssemblies()
    {
        var types = _assemblyService
            .LoadAssemblyTypes(files: PluginFiles)
            .Where(x => x.IsSubclassOf(typeof(DCMPlugin)));
        PluginTypes.AddRange(collection: types);
    }

    private DCMPlugin PropagatePlugin(DCMPlugin plugin, IServiceCollection services)
    {
        plugin.Client = _discordService.Client;
        plugin.Events = _eventService;
        plugin.Services = services;

        return plugin;
    }
}