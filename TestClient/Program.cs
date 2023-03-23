using DCM;
using DCM.Core;
using DCM.Core.Attributes;
using DCM.Core.Interfaces;
using DCM.Core.Models;
using DCM.Extensions;

namespace TestClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new DiscordManager()
            .AddPlugin<HelloWorldDCMPlugin>()
            .UseConfig<Config>("configuration.json")
            .StartAndWait();
    }
}

public class Config : DCMConfig
{
}

public class HelloWorldDCMPlugin : DCMPlugin
{
    private readonly WorldService _service;

    public HelloWorldDCMPlugin(
        WorldService service)
    {
        _service = service;
    }

    public override async Task PostStartAsync()
    {
        await _service.WriteIT();
    }
}

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