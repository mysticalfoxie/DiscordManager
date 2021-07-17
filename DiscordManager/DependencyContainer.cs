using Microsoft.Extensions.DependencyInjection;

namespace DCM
{
    internal class DependencyContainer
    {
        public DependencyContainer(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; set; }
    }
}
