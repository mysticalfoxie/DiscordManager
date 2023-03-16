using System.Threading.Tasks;

namespace DCM.Interfaces;

public interface IConfig<T>
{
    abstract T LoadConfig();
    abstract Task<T> LoadConfigAsync();
}