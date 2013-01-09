using System.Threading.Tasks;

namespace Nocto
{
    public interface IRepositoriesEndpoint
    {
        Task<PagedList<Repository>> GetAllAsync(RepositoryQuery query = null);
    }
}
