using System;
using Microsoft.Extensions.DependencyInjection;

namespace DCM.Extensions;

public static class DependencyServiceExtensions
{
    public static DiscordManager AddService<TService>(this DiscordManager dcm, TService instance) where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddSingleton(implementationInstance: instance);

        return dcm;
    }

    public static DiscordManager AddService<TService>(this DiscordManager dcm) where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddSingleton<TService>();

        return dcm;
    }

    public static DiscordManager AddService<TService>(this DiscordManager dcm, Func<IServiceProvider, TService> fn)
        where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddScoped(implementationFactory: fn);

        return dcm;
    }


    public static DiscordManager AddService<TService>(this DiscordManager dcm, Func<TService> fn) where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddScoped(prov => fn());

        return dcm;
    }

    public static DiscordManager AddService<TService, TImplementation>(this DiscordManager dcm)
        where TImplementation : class, TService where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddSingleton<TService, TImplementation>();

        return dcm;
    }

    public static DiscordManager AddService<TService, TImplementation>(this DiscordManager dcm,
        Func<TImplementation> fn) where TImplementation : class, TService where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddScoped<TService, TImplementation>(prov => fn());

        return dcm;
    }

    public static DiscordManager AddService<TService, TImplementation>(this DiscordManager dcm,
        Func<IServiceProvider, TImplementation> fn) where TImplementation : class, TService where TService : class
    {
        dcm.Services.DependencyService.Services
            .AddScoped<TService, TImplementation>(implementationFactory: fn);

        return dcm;
    }
}