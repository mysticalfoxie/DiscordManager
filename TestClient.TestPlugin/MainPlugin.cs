using DCM.Core;

namespace TestClient.TestPlugin;

public class MainPlugin : DCMPlugin
{
    private readonly WorldService _service;

    public MainPlugin(WorldService service)
    {
        _service = service;
    }

    public override async Task PostStartAsync()
    {
        await _service.WriteIT();
    }
}