using System;
using Discord;
using DiscordManager.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DCM;

public interface IDCMContext
{
    IDiscordClient Client { get; }
    IEventAggregator Events { get; }
    T GetService<T>();
    T GetConfig<T>() where T : class;
}

internal class DCMContext : IDCMContext
{
    private readonly IServiceProvider _services;

    public DCMContext(
        IDiscordClient client,
        IEventAggregator events,
        DependencyContainer services)
    {
        _services = services?.Services.BuildServiceProvider();

        Client = client;
        Events = events;
    }

    public IDiscordClient Client { get; }
    public IEventAggregator Events { get; }

    public T GetService<T>()
    {
        return _services.GetRequiredService<T>();
    }

    public T GetConfig<T>() where T : class
    {
        return _services.GetRequiredService<IOptions<T>>().Value;
    }
}