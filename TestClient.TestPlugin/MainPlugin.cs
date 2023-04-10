using DCM.Core;
using DCM.Core.Attributes;

namespace TestClient.TestPlugin;

[PluginConfig(typeof(MainPluginConfig), "PluginConfig.json")]
public class MainPlugin : DCMPlugin
{
    private readonly WorldService _service;

    public MainPlugin(
        WorldService service)
    {
        _service = service;
    }

    public override async Task PostStartAsync()
    {
        await _service.WriteIT();
    }
}