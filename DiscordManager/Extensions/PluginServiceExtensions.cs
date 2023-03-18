using System;
using System.IO;
using DCM.Core;

namespace DCM.Extensions;

public static class PluginServiceExtensions
{
    public static DiscordManager AddPlugin(this DiscordManager dcm, PluginBase instance)
    {
        if (instance is null)
            throw new ArgumentNullException(nameof(instance));

        var instances = dcm.Services.PluginService.PluginInstances;
        instances.Add(item: instance);

        return dcm;
    }

    public static DiscordManager AddPlugin(this DiscordManager dcm, FileInfo file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        var files = dcm.Services.PluginService.PluginFiles;
        files.Add(item: file);

        return dcm;
    }

    public static DiscordManager AddPlugin(this DiscordManager dcm, Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var types = dcm.Services.PluginService.PluginTypes;
        types.Add(item: type);

        return dcm;
    }

    public static DiscordManager AddPlugin<TPlugin>(this DiscordManager dcm) where TPlugin : PluginBase
    {
        var types = dcm.Services.PluginService.PluginTypes;
        types.Add(typeof(TPlugin));

        return dcm;
    }
}