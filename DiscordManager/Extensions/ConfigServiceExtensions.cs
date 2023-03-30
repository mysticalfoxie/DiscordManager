using System;
using System.IO;
using DCM.Core.Models;

namespace DCM.Extensions;

public static class ConfigServiceExtensions
{
    public static DiscordManager UseDCMConfig<TConfig>(
        this DiscordManager dcm,
        string filename)
        where TConfig : DCMConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        return dcm.UseDCMConfig<TConfig>(new FileInfo(fileName: filename));
    }

    public static DiscordManager UseDCMConfig<TConfig>(
        this DiscordManager dcm,
        FileInfo file)
        where TConfig : DCMConfig
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        dcm.Services.ConfigService.AddDCMConfig<TConfig>(config: file);

        return dcm;
    }

    public static DiscordManager UseGuildConfig<T>(this DiscordManager dcm, string filename)
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));
    }

    private static void MapDCMConfig(DiscordManager dcm, DCMConfig data)
    {
    }
}