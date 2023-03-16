using System;

namespace DCM;

public class ConfigDescriptor<T> where T : class
{
    private readonly T _config;
    
    public ConfigDescriptor(T config)
    {
        _config = config;
    }
    
    internal string LoginToken { get; set; }
    internal ulong? DefaultGuild { get; set; }
    
    public ConfigDescriptor<T> HasLoginToken(Func<T, string> selector)
    {
        LoginToken = selector(_config);
        return this;
    }

    public ConfigDescriptor<T> HasDefaultGuild(Func<T, ulong> selector)
    {
        DefaultGuild = selector(_config);
        return this;
    }
}