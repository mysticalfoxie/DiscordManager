using DCM.Core.Interfaces;
using DCM.Core.Models;

namespace DCM.Core.Builder;

public class DefaultConfigBuilder<TConfig> : IBuilder<DefaultConfig> where TConfig : class
{
    private readonly DefaultConfig _defaultConfig = new();
    private readonly TConfig _config;

    public DefaultConfigBuilder(TConfig config)
    {
        _config = config;
    }


    public DefaultConfigBuilder<TConfig> HasLoginToken(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _defaultConfig.LoginToken = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasDefaultGuild(Func<TConfig, ulong> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _defaultConfig.DefaultGuild = selector(arg: _config);
        return this;
    }

    public DefaultConfig Build()
    {
        return _defaultConfig;
    }
}