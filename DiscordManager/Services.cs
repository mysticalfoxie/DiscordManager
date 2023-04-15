using System;
using DCM.Core.Interfaces;
using DCM.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DCM;

internal class Services
{
    private IServiceProvider _provider;

    public Services()
    {
        Collection = new ServiceCollection()
            .AddSingleton<IConfigService, ConfigService>()
            .AddSingleton<IDependencyService, DependencyService>()
            .AddSingleton<ICredentialsService, CredentialsService>()
            .AddSingleton<IAssemblyService, AssemblyService>()
            .AddSingleton<IPluginService, PluginService>()
            .AddSingleton<IDiscordClientService, DiscordClientService>()
            .AddSingleton<IDiscordService, DiscordService>()
            .AddSingleton<IEventService, EventService>()
            .AddSingleton<IGuildService, GuildService>();
    }

    public IServiceProvider Provider => _provider ??= Collection.BuildServiceProvider();

    public IServiceCollection Collection { get; }

    public ILogger<DiscordManager> Logger { get; private set; }
    public IConfigService ConfigService => Provider.GetRequiredService<IConfigService>();
    public IDependencyService DependencyService => Provider.GetRequiredService<IDependencyService>();
    public ICredentialsService CredentialsService => Provider.GetRequiredService<ICredentialsService>();
    public IAssemblyService AssemblyService => Provider.GetRequiredService<IAssemblyService>();
    public IPluginService PluginService => Provider.GetRequiredService<IPluginService>();
    public IDiscordClientService DiscordClientService => Provider.GetRequiredService<IDiscordClientService>();
    public IEventService EventService => Provider.GetRequiredService<IEventService>();

    public void ConfigureLogging(Action<ILoggingBuilder> configure)
    {
        _provider = Collection
            .AddLogging(configure)
            .BuildServiceProvider();

        Logger = Provider.GetService<ILogger<DiscordManager>>();
    }
}