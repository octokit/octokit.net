using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burr
{
    public interface IAuthorizationsEndpoint
    {
        Task<IEnumerable<Authorization>> GetAllAsync();
        Task<Authorization> GetAsync(int id);
        Task<Authorization> UpdateAsync(int id, AuthorizationUpdate auth);
    }
}
