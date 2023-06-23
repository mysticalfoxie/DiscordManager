using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DCM.Core;

public abstract class ServiceContainer
{
    public ILogger Logger { get; internal set; }
    public IEventService Events { get; internal set; }
    public IDiscordClient Client { get; internal set; }

    public DCMGuildConfig GuildConfig { get; internal set; }
    public DCMGlobalConfig GlobalConfig { get; internal set; }
    public DCMDiscordConfig DiscordConfig { get; internal set; }

    internal IDiscordService DiscordService { get; set; }
    internal IDependencyService DependencyService { get; set; }
    internal IServiceCollection Services { get; set; }

    public void Add<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        Services.AddSingleton<TService, TImplementation>();
    }

    public void Add<TImplementation>() where TImplementation : class
    {
        Services.AddSingleton<TImplementation>();
    }

    public void Add(Type service, Type implementation)
    {
        Services.AddSingleton(service, implementation);
    }

    public void Add(Type implementation)
    {
        Services.AddSingleton(implementation);
    }

    public T Get<T>() where T : class
    {
        return DependencyService.CreateInstance<T>(Services);
    }

    public object Get(Type type)
    {
        return DependencyService.CreateInstance(type, Services);
    }
}