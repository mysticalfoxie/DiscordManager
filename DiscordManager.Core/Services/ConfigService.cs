using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DCM.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DCM.Core.Services;

public class ConfigService : IConfigService
{
    private readonly DependencyService _dependencyService;

    public ConfigService(
        DependencyService dependencyService)
    {
        _dependencyService = dependencyService;
    }

    public ulong? DefaultGuild { get; set; }
    public Dictionary<Type, FileInfo> Configs { get; } = new();
    public Dictionary<Type, object> Instances { get; } = new();


    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public void AddConfig<T>(FileInfo file) where T : class
    {
        if (Configs.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same type for a configuration twice!");

        Configs.Add(typeof(T), value: file);
        var config = ReadConfig<T>();
        _dependencyService.Services
            .Configure<T>(options => options = config);
    }

    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public void AddConfig<T>(T instance) where T : class
    {
        if (Instances.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same type for a configuration twice!");

        Instances.Add(typeof(T), value: instance);
        var config = ReadConfig<T>();
        _dependencyService.Services
            .Configure<T>(options => options = config);
    }

    public T ReadConfig<T>() where T : class
    {
        if (!Configs.ContainsKey(typeof(T)))
            throw new InvalidOperationException("Could not find the configuration type.");

        var value = Configs[typeof(T)];
        var json = File.ReadAllText(path: value.FullName);

        return JsonConvert.DeserializeObject<T>(value: json);
    }
}