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
    private readonly IConfigService _configService;
    private readonly IDependencyService _dependencyService;
    private readonly IDiscordService _discordService;
    private readonly IEventService _eventService;
    private readonly ILogger<PluginService> _logger;

    public PluginService(
        IDependencyService dependencyService,
        IEventService eventService,
        IDiscordService discordService,
        IConfigService configService,
        ILogger<PluginService> logger,
        IAssemblyService assemblyService)
    {
        _dependencyService = dependencyService;
        _eventService = eventService;
        _discordService = discordService;
        _configService = configService;
        _logger = logger;
        _assemblyService = assemblyService;
    }

    public List<DirectoryInfo> PluginDirectories { get; } = new();
    public List<FileInfo> PluginFiles { get; } = new();
    public List<DCMPlugin> PluginInstances { get; } = new();
    public List<Type> PluginTypes { get; } = new();

    public void Invoke(PluginInvokationTarget target)
    {
        var names = GetMethodNames(target);

        _logger.LogTrace($"invoking '{Enum.GetName(target)}' method for all plugins");

        foreach (var plugin in PluginInstances)
        {
            InvokeSynchronousMethod(plugin, names.SyncMethodName);
            InvokeAsynchronousMethod(plugin, names.AsyncMethodName);
        }

        _logger.LogTrace($"invocation of '{Enum.GetName(target)}' method completed");
    }

    public void Load()
    {
        LoadAssembliesFromDirectories();
        LoadPluginTypesFromAssembly();
        InstantiatePlugins();

        _logger.LogInformation($"{PluginInstances.Count} plugin(s) loaded");
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
            var injectables = _dependencyService.SearchInjectables(type.Assembly);
            pluginServices.Add(injectables);

            var plugin = (DCMPlugin)_dependencyService.CreateInstance(type, pluginServices);

            _logger.LogTrace($"plugin {type.FullName} instantiated");

            return plugin;

            // TODO:  return PropagatePlugin(plugin, pluginServices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"could not instantiate a plugin of type '{type.FullName}'");
            return null;
        }
    }

    private void InstantiatePlugins()
    {
        var plugins = PluginTypes
            .Select(InstantiatePlugin)
            .Where(x => x is not null);
        PluginInstances.AddRange(plugins);
    }

    private void InvokeAsynchronousMethod(object instance, string methodName)
    {
        Task.Factory.StartNew(async () =>
        {
            try
            {
                var method = instance.GetType().GetMethod(methodName);
                var task = (Task)method!.Invoke(instance, Array.Empty<object>())!;
                await task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured invoking '{methodName}' in plugin '{instance.GetType().Name}'");
            }
        });
    }

    private void InvokeSynchronousMethod(object instance, string methodName)
    {
        Task.Factory.StartNew(() =>
        {
            try
            {
                var method = instance.GetType().GetMethod(methodName);
                method!.Invoke(instance, Array.Empty<object>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured invoking '{methodName}' in plugin '{instance.GetType().Name}'");
            }
        });
    }

    private void LoadAssembliesFromDirectories()
    {
        var files = PluginDirectories
            .SelectMany(directory =>
            {
                var files = _assemblyService
                    .FindAssemblyFiles(directory)
                    .ToArray();

                _logger.LogTrace($"found {files.Length} library files(s) in source '{directory.FullName}'");
                return files;
            });

        PluginFiles.AddRange(files);
    }

    private void LoadPluginTypesFromAssembly()
    {
        var types = _assemblyService
            .LoadAssemblyTypes(PluginFiles)
            .Where(x => x.IsSubclassOf(typeof(DCMPlugin)))
            .ToArray();

        _logger.LogTrace($"found {types.Length} plugin(s) in '{PluginFiles.Count}' file(s)");
        PluginTypes.AddRange(types);
    }

    private DCMPlugin PropagatePlugin(DCMPlugin plugin, IServiceCollection services)
    {
        plugin.GuildConfig = _configService.GuildConfig;
        plugin.DiscordConfig = _configService.DiscordConfig;
        plugin.GlobalConfig = _configService.GlobalConfig;
        plugin.Client = _discordService.Client;
        plugin.Events = _eventService;
        plugin.Services = services;

        return plugin;
    }
}