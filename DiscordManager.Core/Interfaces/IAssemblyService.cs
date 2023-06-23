namespace DCM.Core.Interfaces;

public interface IAssemblyService
{
    IEnumerable<FileInfo> FindAssemblyFiles(DirectoryInfo directory);
    IEnumerable<Type> LoadAssemblyTypes(IEnumerable<FileInfo> files);
}