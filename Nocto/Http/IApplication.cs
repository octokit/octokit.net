using System.Threading.Tasks;

namespace Octopi.Http
{
    public interface IApplication
    {
        Task<IApplication> Invoke<T>(Environment<T> environment);
    }
}
