namespace DiscordManager.Core.Interfaces;

public interface IAssemblyService
{
    IEnumerable<Type> LoadAssemblyTypes(IEnumerable<FileInfo> files);
}