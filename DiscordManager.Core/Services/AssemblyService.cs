using System.Reflection;
using DiscordManager.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace DiscordManager.Core.Services;

public class AssemblyService : IAssemblyService
{
    private readonly ILogger<AssemblyService> _logger;

    public AssemblyService(
        ILogger<AssemblyService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<Type> LoadAssemblyTypes(IEnumerable<FileInfo> files)
    {
        foreach (var file in files)
        {
            if (!TryLoadAssembly(file: file, out var assembly))
                continue;

            foreach (var type in assembly.GetExportedTypes())
                yield return type;
        }
    }

    // ReSharper disable once SuggestBaseTypeForParameter
    private bool TryLoadAssembly(FileInfo file, out Assembly assembly)
    {
        try
        {
            assembly = Assembly.LoadFrom(assemblyFile: file.FullName);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log(logLevel: LogLevel.Error, exception: ex, "Failed to load assembly.");
            assembly = null;
            return false;
        }
    }
}