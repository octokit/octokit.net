using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi
{
    public interface IAuthorizationsClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        Task<IReadOnlyCollection<Authorization>> GetAll();
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<Authorization> Get(long id);
        Task<Authorization> Update(long id, AuthorizationUpdate authorization);
        Task<Authorization> Create(AuthorizationUpdate authorization);
        Task Delete(long id);
    }
}
