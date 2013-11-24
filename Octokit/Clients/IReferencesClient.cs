using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IReferencesClient
    {
        Task<Reference> Get(string owner, string name, string reference);

        Task<IReadOnlyList<Reference>> GetAll(string owner, string name, string subNamespace = null);

        Task<Reference> Create(string owner, string name, NewReference reference);

        Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate update);

        Task Delete(string owner, string name, string reference);
    }
}