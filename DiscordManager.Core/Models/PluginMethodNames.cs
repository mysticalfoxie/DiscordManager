namespace DiscordManager.Core.Models;

internal class PluginMethodNames
{
    public PluginMethodNames(
        string syncMethodName,
        string asyncMethodName)
    {
        SyncMethodName = syncMethodName;
        AsyncMethodName = asyncMethodName;
    }

    public string SyncMethodName { get; }
    public string AsyncMethodName { get; }
}