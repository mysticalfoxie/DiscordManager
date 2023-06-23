namespace DCM.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class PluginConfigAttribute : Attribute
{
    public PluginConfigAttribute(Type type, object value)
    {
        Type = type;
        Value = value;
    }

    public PluginConfigAttribute(Type type, FileInfo file)
    {
        Type = type;
        File = file;
    }

    public PluginConfigAttribute(Type type, string filepath)
    {
        Type = type;
        Filepath = filepath;
    }

    public Type Type { get; }
    public object Value { get; }
    public FileInfo File { get; }
    public string Filepath { get; }
}