using DCM.Core;
using DCM.Core.Attributes;
using DCM.Core.Interfaces;

namespace TestClient.TestPlugin;

[Injectable]
public class WorldService : ServiceContainer
{
    private readonly IDiscordService _service;

    public WorldService(
        IDiscordService service)
    {
        _service = service;
    }

    public async Task WriteIT()
    {
        Console.WriteLine("Hello World!");
        await _service.StopAsync();
    }
}