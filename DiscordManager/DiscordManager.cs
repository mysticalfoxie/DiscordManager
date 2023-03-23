using System;
using System.Threading.Tasks;
using DCM.Core.Enums;
using Microsoft.Extensions.Logging;

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
        Task.Factory.StartNew(async () =>
        {
            try
            {
                await StartInternal();
            }
            catch (Exception ex)
            {
                Services.Logger.Log(logLevel: LogLevel.Critical, exception: ex,
                    "An error occured during start pipeline.");
                throw;
            }
        });
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

    // TODO: This has to be done to have legacy support!
    // public DiscordManager Configure(Action<IDiscordConfigBuilder> configure)
    // public DiscordManager AddPluginDirectory(DirectoryInfo directory)
    // public DiscordManager AddPluginCollector(AssemblyCollector pluginCollector)

    private async Task StartInternal()
    {
        Services.DependencyService.PublishServices(
            discordService: Services.DiscordService,
            eventService: Services.EventService);
        Services.PluginService.Load();
        Services.DiscordService.Build();
        Services.PluginService.Invoke(target: PluginInvokationTarget.PreStart);
        Services.EventService.MapEvents();
        await Services.DiscordService.StartAsync();
        Services.PluginService.Invoke(target: PluginInvokationTarget.PostStart);
    }
}