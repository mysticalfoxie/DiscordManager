using System.Reflection;
using DiscordManager.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordManager.Core.Services;

internal class DependencyService : IDependencyService
{
    public DependencyService(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; set; }
    public IServiceProvider Provider => Services.BuildServiceProvider();

    public T CreateInstantiate<T>()
    {
        return (T)CreateInstantiate(typeof(T));
    }

    public object CreateInstantiate(Type type)
    {
        var constructor = GetConstructor(type: type);
        var parameters = GetParameters(constructor: constructor);
        return constructor.Invoke(parameters: parameters);
    }

    private object[] GetParameters(MethodBase constructor)
    {
        return constructor
            .GetParameters()
            .Select(x => x.ParameterType)
            .Select(x => Provider.GetService(serviceType: x))
            .ToArray();
    }

    private static ConstructorInfo GetConstructor(Type type)
    {
        var constructors = type.GetConstructors(bindingAttr: BindingFlags.Public);
        return constructors.Length switch
        {
            > 1 => throw new InvalidOperationException("Multiple Constructors were found! Cannot create an instance!"),
            0 => throw new InvalidOperationException("No Constructor was found! Cannot create an instance!"),
            _ => constructors.First()
        };
    }
}