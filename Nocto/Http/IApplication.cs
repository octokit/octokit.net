using System.Threading.Tasks;

namespace Nocto.Http
{
    public interface IApplication
    {
        Task<IApplication> Call<T>(Environment<T> environment);
    }
}
