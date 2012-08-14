using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burr
{
    public interface IRepositoriesEndpoint
    {
        Task<List<Repository>> GetAllAsync(RepositoryQuery query = null);
    }
}