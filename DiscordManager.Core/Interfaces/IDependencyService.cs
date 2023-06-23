using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DCM.Core.Interfaces;

public interface IDependencyService
{
    IServiceCollection Services { get; }
    object CreateInstance(Type type, IServiceCollection secondary = null);
    T CreateInstance<T>(IServiceCollection secondary = null) where T : class;

    void PublishServices(
        IDiscordClientService discordClientService,
        IEventService eventService);

    IEnumerable<ServiceDescriptor> SearchInjectables(Assembly assembly);
    IEnumerable<ServiceDescriptor> SearchPluginServices(Assembly typeAssembly);
}