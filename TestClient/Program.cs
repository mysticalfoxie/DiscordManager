using DCM;
using DCM.Extensions;

namespace TestClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new DiscordManager()
            //.AddPlugin(@"C:\Source\DiscordManager\TestClient.TestPlugin\bin\Debug\net7.0\TestClient.TestPlugin.dll")
            .UseConfig<Config>("configuration.json")
            .StartAndWait();
    }
}