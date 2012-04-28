using System.Threading.Tasks;

namespace Burr.Http
{
    public interface IApplication
    {
        Task<IApplication> Call<T>(Env<T> env);
    }
}
