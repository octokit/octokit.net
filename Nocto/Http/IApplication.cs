using System.Threading.Tasks;

namespace Nocto.Http
{
    public interface IApplication
    {
        Task<IApplication> Call<T>(Env<T> env);
    }
}
