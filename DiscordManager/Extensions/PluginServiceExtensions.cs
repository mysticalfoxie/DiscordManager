using System;
using System.Collections.Generic;
using System.IO;
using DCM.Core;

namespace DCM.Extensions;

public static class PluginServiceExtensions
{
    public static DiscordManager AddPlugin(this DiscordManager dcm, DCMPlugin instance)
    {
        if (instance is null)
            throw new ArgumentNullException(nameof(instance));

        var instances = dcm.Services.PluginService.PluginInstances;
        instances.Add(instance);

        return dcm;
    }

    public static DiscordManager AddPlugin(this DiscordManager dcm, Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var types = dcm.Services.PluginService.PluginTypes;
        types.Add(type);

        return dcm;
    }

    public static DiscordManager AddPlugin<TPlugin>(this DiscordManager dcm) where TPlugin : DCMPlugin
    {
        var types = dcm.Services.PluginService.PluginTypes;
        types.Add(typeof(TPlugin));

        return dcm;
    }

    public static DiscordManager AddPlugins(this DiscordManager dcm, IEnumerable<string> paths)
    {
        if (paths is null)
            throw new ArgumentNullException(nameof(paths));

        foreach (var path in paths)
            AddPlugins(dcm, path);

        return dcm;
    }

    public static DiscordManager AddPlugins(this DiscordManager dcm, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        if (!File.Exists(path) && !Directory.Exists(path))
            throw new FileNotFoundException(nameof(path));

        var attributes = File.GetAttributes(path) & FileAttributes.Directory;
        if (attributes == FileAttributes.Directory)
        {
            var directory = new DirectoryInfo(path);
            dcm.Services.PluginService.PluginDirectories.Add(directory);
        }
        else
        {
            var file = new FileInfo(path);
            dcm.Services.PluginService.PluginFiles.Add(file);
        }

        return dcm;
    }

    public static DiscordManager AddPlugins(this DiscordManager dcm, DirectoryInfo directory)
    {
        if (directory is null)
            throw new ArgumentNullException(nameof(directory));

        if (!directory.Exists)
            throw new DirectoryNotFoundException(nameof(directory));

        dcm.Services.PluginService.PluginDirectories.Add(directory);

        return dcm;
    }
}