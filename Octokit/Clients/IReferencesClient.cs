using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IReferencesClient
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Reference> Get(string owner, string name, string reference);

        Task<IReadOnlyList<Reference>> GetAll(string owner, string name);

        Task<IReadOnlyList<Reference>> GetAll(string owner, string name, string subNamespace);

        Task<Reference> Create(string owner, string name, NewReference reference);

        Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate);

        Task Delete(string owner, string name, string reference);
    }
}