using System.Reflection;
using DCM.Core.Attributes;
using DCM.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DCM.Core.Services;

public class DependencyService : IDependencyService
{
    public IServiceCollection Services { get; } = new ServiceCollection();

    public object CreateInstance(Type type, IServiceCollection dependencies)
    {
        var constructor = GetConstructor(type);
        if (constructor is null)
            return Activator.CreateInstance(type);

        var parameters = GetParameters(constructor, dependencies);
        return constructor.Invoke(parameters);
    }

    public T CreateInstance<T>(IServiceCollection collection) where T : class
    {
        return (T)CreateInstance(typeof(T), collection);
    }

    public void PublishServices(
        IDiscordClientService discordClientService,
        IEventService eventService)
    {
        Services.AddSingleton(discordClientService);
        Services.AddSingleton(eventService);
    }

    public IEnumerable<ServiceDescriptor> SearchInjectables(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(x => x.IsClass)
            .Select(x => new
            {
                Attribute = x.GetCustomAttribute<InjectableAttribute>(),
                ServiceType = x
            })
            .Where(x => x.Attribute is not null)
            .Select(x => x.Attribute.Interface is not null
                ? new ServiceDescriptor(x.Attribute.Interface, x.ServiceType, ServiceLifetime.Singleton)
                : new ServiceDescriptor(x.ServiceType, ServiceLifetime.Singleton));
    }

    public IEnumerable<ServiceDescriptor> SearchPluginServices(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(x => x.IsClass)
            .Where(x => x.IsAssignableTo(typeof(DCMPluginService)))
            .Select(x => new
            {
                Interface = x.GetInterfaces().FirstOrDefault(),
                ServiceType = x
            })
            .Select(x => x.Interface is not null
                ? new ServiceDescriptor(x.Interface, x.ServiceType, ServiceLifetime.Singleton)
                : new ServiceDescriptor(x.ServiceType, ServiceLifetime.Singleton));
    }

    private static ConstructorInfo GetConstructor(Type type)
    {
        var constructors = type.GetConstructors();
        return constructors.Length switch
        {
            > 1 => throw new InvalidOperationException("Multiple Constructors were found! Cannot create an instance!"),
            0 => null,
            _ => constructors.First()
        };
    }


    private object[] GetParameters(ConstructorInfo constructor, IServiceCollection secondarySource)
    {
        var provider = MergeServices(secondarySource);
        return constructor
            .GetParameters()
            .Select(x => x.ParameterType)
            .Select(x => provider.GetService(x))
            .ToArray();
    }

    private ServiceProvider MergeServices(IServiceCollection secondary)
    {
        var source = new ServiceCollection();
        var services = source.Concat(Services).Concat(secondary);
        source.Add(services);
        var provider = source.BuildServiceProvider();
        return provider;
    }
}