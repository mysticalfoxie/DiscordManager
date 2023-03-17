using DiscordManager.Core.Enums;

namespace DiscordManager.Core.Interfaces;

internal interface IPluginManager
{
    List<FileInfo> PluginFiles { get; }
    List<PluginBase> PluginInstances { get; }
    List<Type> PluginTypes { get; }
    void Invoke(PluginInvokationTarget target);
    int Load();
}