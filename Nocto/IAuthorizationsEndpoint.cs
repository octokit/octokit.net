using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi
{
    public interface IAuthorizationsEndpoint
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        Task<List<Authorization>> GetAll();
        Task<Authorization> GetAsync(int id);
        Task<Authorization> UpdateAsync(int id, AuthorizationUpdate authorization);
        Task<Authorization> CreateAsync(AuthorizationUpdate authorization);
        Task DeleteAsync(int id);
    }
}
