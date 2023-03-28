using DCM;
using TestClient.TestPlugin;

namespace TestClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new DiscordManager()
            .AddPlugin(@"C:\Source\DiscordManager\TestClient.TestPlugin\bin\Debug\net7.0\TestClient.TestPlugin.dll")
            .UseDCMConfig<GlobalConfig>("global_config.json")
            .UseGlobalConfig<GuildConfig>("guild_config.json")
            .UseDiscordConfig<DCConfig>("discord_config.json")
            .StartAndWait();
    }
}