namespace DCM.Core.Models;

public class DCMPluginConfig
{
    internal Dictionary<string, object> Entries { get; } = new();
    public object this[string i] => Entries[i];
}