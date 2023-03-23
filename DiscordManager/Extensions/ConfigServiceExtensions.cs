using System;
using System.IO;
using DCM.Core.Builder;
using DCM.Core.Models;

namespace DCM.Extensions;

public static class ConfigServiceExtensions
{
    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        string filename)
        where TConfig : class
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        return dcm.AddConfig<TConfig>(new FileInfo(fileName: filename), null);
    }

    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        FileInfo file)
        where TConfig : class
    {
        return dcm.AddConfig<TConfig>(file: file, null);
    }

    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        TConfig config)
        where TConfig : class
    {
        return dcm.AddConfig(config: config, null);
    }

    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        string filename,
        Action<DefaultConfigBuilder<TConfig>> configure)
        where TConfig : class
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        return dcm.AddConfig(new FileInfo(fileName: filename), configure: configure);
    }

    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        FileInfo file,
        Action<DefaultConfigBuilder<TConfig>> configure)
        where TConfig : class
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        dcm.Services.ConfigService.AddConfig<TConfig>(file: file);

        if (configure is null)
            return dcm;

        ConfigureDefaultConfig(dcm: dcm, configure: configure);

        return dcm;
    }

    public static DiscordManager AddConfig<TConfig>(
        this DiscordManager dcm,
        TConfig config,
        Action<DefaultConfigBuilder<TConfig>> configure)
        where TConfig : class
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        dcm.Services.ConfigService.AddConfig(config: config);

        if (configure is null)
            return dcm;

        ConfigureDefaultConfig(dcm: dcm, configure: configure);

        return dcm;
    }

    public static DiscordManager UseConfig<TConfig>(
        this DiscordManager dcm,
        string filename)
        where TConfig : DCMConfig
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        return dcm.UseConfig<TConfig>(new FileInfo(fileName: filename));
    }

    public static DiscordManager UseConfig<TConfig>(
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

        MapDefaultConfig(dcm: dcm, data: config);
        return dcm;
    }

    private static void ConfigureDefaultConfig<TConfig>(
        DiscordManager dcm,
        Action<DefaultConfigBuilder<TConfig>> configure)
        where TConfig : class
    {
        var config = dcm.Services.ConfigService.ReadConfig<TConfig>();
        var builder = new DefaultConfigBuilder<TConfig>(config: config);
        configure(obj: builder);
        var data = builder.Build();

        MapDefaultConfig(dcm: dcm, data: data);
    }

    private static void MapDefaultConfig(DiscordManager dcm, DCMConfig data)
    {
        if (data.DefaultGuild.HasValue && data.DefaultGuild.Value != default)
            dcm.Services.ConfigService.DefaultGuild = data.DefaultGuild.Value;

        if (!string.IsNullOrWhiteSpace(value: data.LoginToken))
            dcm.Services.CredentialsService.LoginToken = data.LoginToken;

        if (data.DiscordConfig is not null)
            dcm.Services.ConfigService.DiscordConfig = data.DiscordConfig;
    }
}