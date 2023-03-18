using Microsoft.Extensions.DependencyInjection;

namespace DCM.Core.Interfaces;

public interface IDependencyService
{
    IServiceCollection Services { get; set; }
    T CreateInstance<T>();
    object CreateInstance(Type type);
}