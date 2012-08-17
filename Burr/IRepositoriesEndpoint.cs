using System.Threading.Tasks;

namespace Burr
{
    public interface IRepositoriesEndpoint
    {
        Task<PagedList<Repository>> GetAllAsync(RepositoryQuery query = null);
    }
}
