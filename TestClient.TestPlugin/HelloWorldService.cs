using DCM.Core;
using DCM.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace TestClient.TestPlugin;

public class WorldService : DCMPluginService, IWorldService
{
    private readonly IDiscordClientService _service;

    public WorldService(
        IDiscordClientService service)
    {
        _service = service;
    }

    public async Task WriteIt()
    {
        Logger.LogInformation("Hello World!");
        await _service.StopAsync();
    }
}

public interface IWorldService
{
    public Task WriteIt();
}