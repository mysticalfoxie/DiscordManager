using DCM;
using DCM.Extensions;

namespace TestClient;

public class Program
{
    public static async Task Main()
    {
        await new DiscordManager()
            .AddPlugins(@"C:\Source\Remote\DiscordManager\TestClient.TestPlugin\bin\Debug")
            .AddPlugins(@"C:\Source\Remote\DiscordManager\TestClient.AnotherPlugin\bin\Debug")
            .UseConfig<Config>("configuration.json")
            .StartAndWait();
    }
}