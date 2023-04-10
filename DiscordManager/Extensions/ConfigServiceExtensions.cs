using System;
using DCM.Core.Models;

namespace DCM.Extensions;

public static class ConfigServiceExtensions
{
    public static DiscordManager AddConfig<T>(this DiscordManager dcm, string filename) where T : class
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        var config = dcm.Services.ConfigService.ReadConfig<T>(filename).Result;
        dcm.Services.ConfigService.AddConfig(config);

        return dcm;
    }

    public static DiscordManager AddConfig<T>(this DiscordManager dcm, T instance) where T : class
    {
        if (instance is null)
            throw new ArgumentNullException(nameof(instance));

        dcm.Services.ConfigService.AddConfig(instance);

        return dcm;
    }

    public static DiscordManager UseConfig<T>(this DiscordManager dcm, string filename) where T : DCMConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        var config = dcm.Services.ConfigService.ReadConfig<T>(filename).Result;
        if (config.Discord is not null)
            dcm.Services.ConfigService.AddDiscordConfig(config.Discord);
        if (config.Guild is not null)
            dcm.Services.ConfigService.AddGuildConfig(config.Guild);

        var dcmConfig = new DCMGlobalConfig { LoginToken = config.LoginToken };
        dcm.Services.ConfigService.AddDCMConfig(dcmConfig);

        return dcm;
    }

    public static DiscordManager UseDiscordConfig<T>(this DiscordManager dcm, string filename)
        where T : DCMDiscordConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        var config = dcm.Services.ConfigService.ReadConfig<T>(filename).Result;
        dcm.Services.ConfigService.AddDiscordConfig(config);

        return dcm;
    }

    public static DiscordManager UseGlobalConfig<T>(this DiscordManager dcm, string filename) where T : DCMGlobalConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        var config = dcm.Services.ConfigService.ReadConfig<T>(filename).Result;
        dcm.Services.ConfigService.AddDCMConfig(config);

        return dcm;
    }

    public static DiscordManager UseGuildConfig<T>(this DiscordManager dcm, string filename) where T : DCMGuildConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        var config = dcm.Services.ConfigService.ReadConfig<T>(filename).Result;
        dcm.Services.ConfigService.AddGuildConfig(config);

        return dcm;
    }
}