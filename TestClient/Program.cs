using DCM;
using DCM.Extensions;
using Microsoft.Extensions.Logging;

namespace TestClient;

public class Program
{
    public static async Task Main()
    {
        await new DiscordManager()
            .ConfigureLogging(x => x.AddConsole())
            .AddPlugins(@"C:\Source\Remote\DiscordManager\TestClient.TestPlugin\bin\Debug")
            .AddPlugins(@"C:\Source\Remote\DiscordManager\TestClient.AnotherPlugin\bin\Debug")
            .UseConfig<Config>("configuration.json")
            .StartAndWait();
    }
}