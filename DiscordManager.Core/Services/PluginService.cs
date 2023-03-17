using DiscordManager.Core.Enums;
using DiscordManager.Core.Interfaces;
using DiscordManager.Core.Models;
using Microsoft.Extensions.Logging;

namespace DiscordManager.Core.Services;

internal class PluginService : IPluginManager
{
    private readonly IDependencyService _dependencyService;
    private readonly ILogger<PluginService> _logger;
    private readonly IAssemblyService _assemblyService;

    public PluginService(
        IDependencyService dependencyService,
        ILogger<PluginService> logger,
        IAssemblyService assemblyService)
    {
        _dependencyService = dependencyService;
        _logger = logger;
        _assemblyService = assemblyService;
    }

    public List<FileInfo> PluginFiles { get; } = new();
    public List<PluginBase> PluginInstances { get; } = new();
    public List<Type> PluginTypes { get; } = new();

    public int Load()
    {
        LoadPluginTypesFromAssemblies();
        InstantiatePlugins();
        return PluginInstances.Count;
    }

    public void Invoke(PluginInvokationTarget target)
    {
        var names = GetMethodNames(target: target);
        foreach (var plugin in PluginInstances)
        {
            InvokeSynchronousMethod(instance: plugin, methodName: names.SyncMethodName);
            InvokeAsynchronousMethod(instance: plugin, methodName: names.AsyncMethodName);
        }
    }

    private void LoadPluginTypesFromAssemblies()
    {
        var types = _assemblyService
            .LoadAssemblyTypes(files: PluginFiles)
            .Where(x => x.IsSubclassOf(typeof(PluginBase)));
        PluginTypes.AddRange(collection: types);
    }

    private PluginBase InstantiatePlugin(Type type)
    {
        try
        {
            return (PluginBase)_dependencyService.CreateInstantiate(type: type);
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

    private static bool IsImplemented(PluginBase instance, string methodName)
    {
        return !instance
            .GetType()
            .GetMethod(name: methodName)!
            .IsAbstract;
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


    private static PluginMethodNames GetMethodNames(PluginInvokationTarget target)
    {
        return target switch
        {
            PluginInvokationTarget.PostStart => new PluginMethodNames(
                nameof(PluginBase.PostStart),
                nameof(PluginBase.PostStartAsync)),
            PluginInvokationTarget.PreStart => new PluginMethodNames(
                nameof(PluginBase.PreStart),
                nameof(PluginBase.PreStartAsync)),
            _ => throw new NotSupportedException()
        };
    }
}