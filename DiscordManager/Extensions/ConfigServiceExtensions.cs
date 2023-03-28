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

        dcm.Services.ConfigService.AddConfig<TConfig>(file: file);
        var config = dcm.Services.ConfigService.ReadConfig<TConfig>();

        if (string.IsNullOrWhiteSpace(value: config.LoginToken))
            throw new NullReferenceException(nameof(config.LoginToken));

        MapDCMConfig(dcm: dcm, data: config);
        return dcm;
    }

    private static void MapDCMConfig(DiscordManager dcm, DCMConfig data)
    {
        if (data.DefaultGuild.HasValue && data.DefaultGuild.Value != default)
            dcm.Services.ConfigService.DefaultGuild = data.DefaultGuild.Value;

        if (!string.IsNullOrWhiteSpace(value: data.LoginToken))
            dcm.Services.CredentialsService.LoginToken = data.LoginToken;
    }
}