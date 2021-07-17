using System.Threading.Tasks;

namespace DCM.Interfaces
{
    public interface IPlugin
    {
        Task PreStartAsync();
        Task StartAsync();
    }
}
