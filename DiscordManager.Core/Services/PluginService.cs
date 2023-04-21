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
    private readonly IDiscordClientService _discordClientService;
    private readonly IDiscordService _discordService;
    private readonly IEventService _eventService;
    private readonly IGuildService _guildService;
    private readonly ILogger<PluginService> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public PluginService(
        IDependencyService dependencyService,
        IEventService eventService,
        IDiscordClientService discordClientService,
        IDiscordService discordService,
        IConfigService configService,
        IGuildService guildService,
        ILoggerFactory loggerFactory,
        ILogger<PluginService> logger,
        IAssemblyService assemblyService)
    {
        _dependencyService = dependencyService;
        _eventService = eventService;
        _discordClientService = discordClientService;
        _discordService = discordService;
        _configService = configService;
        _guildService = guildService;
        _loggerFactory = loggerFactory;
        _logger = logger;
        _assemblyService = assemblyService;
    }

    public List<DirectoryInfo> PluginDirectories { get; } = new();
    public List<FileInfo> PluginFiles { get; } = new();
    public List<Type> PluginServiceTypes { get; } = new();
    public List<DCMPlugin> PluginInstances { get; } = new();
    public List<DCMPluginService> PluginServiceInstances { get; } = new();
    public List<Type> PluginTypes { get; } = new();

    public void Invoke(PluginInvokationTarget target)
    {
        var names = GetMethodNames(target);

        _logger.LogTrace($"Invoking '{Enum.GetName(target)}' method for all plugins");

        foreach (var plugin in PluginInstances)
        {
            InvokeSynchronousMethod(plugin, names.SyncMethodName);
            InvokeAsynchronousMethod(plugin, names.AsyncMethodName);
        }

        _logger.LogTrace($"Invocation of '{Enum.GetName(target)}' method completed");
    }

    public void Load()
    {
        LoadAssembliesFromDirectories();
        LoadDCMTypesFromAssembly();
        InstantiatePluginServices();
        InstantiatePlugins();

        _logger.LogInformation($"{PluginInstances.Count} plugin(s) loaded");
    }

    public void PropagateDCMContainers()
    {
        foreach (var plugin in PluginInstances.Concat<ServiceContainer>(PluginServiceInstances))
        {
            plugin.GuildConfig = _configService.GuildConfig;
            plugin.DiscordConfig = _configService.DiscordConfig;
            plugin.GlobalConfig = _configService.GlobalConfig;
            plugin.Client = _discordClientService.Client;
            plugin.Events = _eventService;
            plugin.DependencyService = _dependencyService;
            plugin.DiscordService = _discordService;
            plugin.Logger = _loggerFactory.CreateLogger(plugin.GetType());
        }
    }

    public async Task PropagateDiscordContainer()
    {
        if (_configService.GuildConfig is not null)
        {
            _logger.LogTrace("Loading all guild objects from configuration");

            await _guildService.Load();
            foreach (var plugin in PluginInstances)
                _guildService.PropagateContainerFromCache(plugin);
            foreach (var service in PluginServiceInstances)
                _guildService.PropagateContainerFromCache(service);

            _logger.LogInformation("Main guild has been downloaded and cached");
        }
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
            var dcmServices = _dependencyService.SearchPluginServices(type.Assembly);
            pluginServices.Add(injectables);
            pluginServices.Add(dcmServices);

            var plugin = (DCMPlugin)_dependencyService.CreateInstance(type, pluginServices);
            plugin.Services = pluginServices;

            _logger.LogTrace($"Plugin {type.Name} instantiated");

            return plugin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Could not instantiate a plugin of type '{type.FullName}'");
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

    private DCMPluginService InstantiatePluginService(Type type)
    {
        try
        {
            var additionalServices = new ServiceCollection();
            var injectables = _dependencyService.SearchInjectables(type.Assembly);
            additionalServices.Add(injectables);

            var service = (DCMPluginService)_dependencyService.CreateInstance(type, additionalServices);

            _logger.LogTrace($"Plugin service {type.FullName} instantiated");

            return service;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Could not instantiate a plugin service of type '{type.FullName}'");
            return null;
        }
    }

    private void InstantiatePluginServices()
    {
        var services = PluginServiceTypes
            .Select(InstantiatePluginService)
            .Where(x => x is not null);
        PluginServiceInstances.AddRange(services);
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
                _logger.LogError(ex, $"An error occured invoking '{methodName}' in plugin '{instance.GetType().Name}'");
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
                _logger.LogError(ex, $"An error occured invoking '{methodName}' in plugin '{instance.GetType().Name}'");
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

                _logger.LogTrace($"Found {files.Length} library files(s) in source '{directory.FullName}'");
                return files;
            });

        PluginFiles.AddRange(files);
    }

    private void LoadDCMTypesFromAssembly()
    {
        var types = _assemblyService
            .LoadAssemblyTypes(PluginFiles)
            .Where(x => x.IsClass)
            .Where(x => !x.IsAbstract)
            .ToArray();

        var plugins = types.Where(x => x.IsAssignableTo(typeof(DCMPlugin))).ToArray();
        var services = types.Where(x => x.IsAssignableTo(typeof(DCMPluginService))).ToArray();

        PluginTypes.AddRange(plugins);
        PluginServiceTypes.AddRange(services);

        _logger.LogTrace(
            $"Found {plugins.Length} plugin(s) and {services.Length} service(s) in {PluginFiles.Count} file(s)");
    }
}