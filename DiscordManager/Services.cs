using System;
using DCM.Core.Interfaces;
using DCM.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DCM;

internal class Services
{
    public Services()
    {
        Provider = new ServiceCollection()
            .AddSingleton<IConfigService, ConfigService>()
            .AddSingleton<IDependencyService, DependencyService>()
            .AddSingleton<ICredentialsService, CredentialsService>()
            .AddSingleton<IAssemblyService, AssemblyService>()
            .AddSingleton<IPluginService, PluginService>()
            .AddSingleton<IDiscordService, DiscordService>()
            .AddSingleton<IEventService, EventService>()
            .AddLogging()
            .BuildServiceProvider();

        ConfigService = Provider.GetService<IConfigService>();
        DependencyService = Provider.GetService<IDependencyService>();
        CredentialsService = Provider.GetService<ICredentialsService>();
        AssemblyService = Provider.GetService<IAssemblyService>();
        PluginService = Provider.GetService<IPluginService>();
        DiscordService = Provider.GetService<IDiscordService>();
        EventService = Provider.GetService<IEventService>();
        Logger = Provider.GetService<ILogger<DiscordManager>>();
    }


    public IServiceProvider Provider { get; }

    public ILogger<DiscordManager> Logger { get; }
    public IConfigService ConfigService { get; }
    public IDependencyService DependencyService { get; }
    public ICredentialsService CredentialsService { get; }
    public IAssemblyService AssemblyService { get; }
    public IPluginService PluginService { get; }
    public IDiscordService DiscordService { get; }
    public IEventService EventService { get; }
}