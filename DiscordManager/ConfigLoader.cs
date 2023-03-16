using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace DCM;

internal class ConfigLoader
{
    private readonly Dictionary<Type, string> _configuration = new();
    private readonly DependencyContainer _container;

    public ConfigLoader(DependencyContainer container)
    {
        _container = container;
    }

    public void AddConfigFile<T>(string filename)
    {
        if (_configuration.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same type for a configuration twice!");

        _configuration.Add(typeof(T), value: filename);

        // TODO: Add to DependencyContainer
    }

    public T ReadConfig<T>()
    {
        if (!_configuration.ContainsKey(typeof(T)))
            throw new InvalidOperationException("Could not find the configuration type.");

        var value = _configuration[typeof(T)];
        var json = File.ReadAllText(path: value);

        return JsonConvert.DeserializeObject<T>(value: json);
    }
}