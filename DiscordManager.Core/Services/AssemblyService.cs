using System.Reflection;
using DCM.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace DCM.Core.Services;

public class AssemblyService : IAssemblyService
{
    private readonly ILogger<AssemblyService> _logger;

    public AssemblyService(
        ILogger<AssemblyService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<FileInfo> FindAssemblyFiles(DirectoryInfo directory)
    {
        return directory
            .EnumerateDirectories()
            .Select(x => new DirectoryInfo(x.FullName))
            .SelectMany(FindAssemblyFiles)
            .Concat(directory
                .EnumerateFiles()
                .Where(x => x.Extension.ToLower() == ".dll"))
            .ToArray();
    }

    public IEnumerable<Type> LoadAssemblyTypes(IEnumerable<FileInfo> files)
    {
        foreach (var file in files)
        {
            if (!TryLoadAssembly(file, out var assembly))
                continue;

            foreach (var type in assembly.GetExportedTypes())
                yield return type;
        }
    }

    private bool TryLoadAssembly(FileInfo file, out Assembly assembly)
    {
        try
        {
            assembly = Assembly.LoadFrom(file.FullName);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex, "Failed to load assembly");
            assembly = null;
            return false;
        }
    }
}