using DCM;
using DCM.Core.Models;
using DCM.Extensions;

#pragma warning disable CA1050

namespace TestClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new DiscordManager()
            .AddConfig<Config>("configuration.json")
            .StartAndWait();
    }
}

public class Config : DefaultConfig
{
}

#pragma warning restore CA1050