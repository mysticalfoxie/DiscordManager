using System;
using System.Threading.Tasks;
using DCM.Core.Enums;

namespace DCM;

public class DiscordManager : IAsyncDisposable
{
    internal Services Services { get; } = new();

    public async ValueTask DisposeAsync()
    {
        await Services.DiscordService.StopAsync();
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
        Services.DependencyService.PublishServices(Services.DiscordService, Services.EventService);
        Services.PluginService.Load();
        await Services.ConfigService.LoadPluginConfigs(Services.PluginService.PluginInstances);
        Services.DiscordService.Build();
        Services.PluginService.Invoke(PluginInvokationTarget.PreStart);
        Services.EventService.MapEvents();
        await Services.DiscordService.StartAsync();
        Services.PluginService.Invoke(PluginInvokationTarget.PostStart);
    }
}