using DCM;

namespace DiscordManager.Tests;

public class Class1
{
    private var discordManager = new DCM.DiscordManager();
}

internal class Test : Plugin
{
    public override void Start()
    {
        Events.MessageReceived
            .Where(x => x.Message.)
            .Subscribe(data => { data.Message; })
    }
}

internal class Foo
{
}