using Microsoft.Extensions.DependencyInjection;

namespace DiscordManager.Core.Interfaces;

internal interface IDependencyService
{
    IServiceCollection Services { get; set; }
    T CreateInstantiate<T>();
    object CreateInstantiate(Type type);
}