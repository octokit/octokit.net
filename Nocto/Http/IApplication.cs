using System.Threading.Tasks;

namespace Nocto.Http
{
    public interface IApplication
    {
        Task<IApplication> Invoke<T>(Environment<T> environment);
    }
}
