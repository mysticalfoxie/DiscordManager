using System;
using System.Threading.Tasks;
using DCM.Core.Enums;

namespace DCM;

public class DiscordManager : IAsyncDisposable
{
    internal Services Services { get; } = new();

    public async ValueTask DisposeAsync()
    {
        await Services.DiscordClientService.StopAsync();
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        Task.Factory.StartNew(StartInternal);
    }

    public async Task StartAndWait()
    {
        await StartAsync();
        await Task.Delay(-1);
    }

    public Task StartAsync()
    {
        return StartInternal();
    }

    private async Task StartInternal()
    {
        Services.DependencyService.PublishServices(Services.DiscordClientService, Services.EventService);
        Services.PluginService.Load();
        Services.PluginService.PropagateDCMContainers();
        await Services.ConfigService.LoadPluginConfigs(Services.PluginService.PluginInstances);
        Services.DiscordClientService.Build();
        Services.PluginService.Invoke(PluginInvokationTarget.PreStart);
        Services.EventService.MapEvents();
        await Services.DiscordClientService.StartAsync();
        await Services.PluginService.PropagateDiscordContainer();
        Services.PluginService.Invoke(PluginInvokationTarget.PostStart);
    }
}