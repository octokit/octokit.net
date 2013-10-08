using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IAuthorizationsClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        Task<IReadOnlyList<Authorization>> GetAll();
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<Authorization> Get(int id);
        Task<Authorization> Update(int id, AuthorizationUpdate authorization);
        Task<Authorization> Create(AuthorizationUpdate authorization);
        Task Delete(int id);
    }
}
