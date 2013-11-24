using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class ReferencesClient : ApiClient, IReferencesClient
    {
        public ReferencesClient(IApiConnection apiConnection) : 
            base(apiConnection)
        {
        }

        public Task<Reference> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Reference>(ApiUrls.Reference(owner, name, reference));
        }

        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name, string subNamespace = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reference> Create(string owner, string name, NewReference reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<Reference> Update(string owner, string name, string reference, string sha, bool force = false)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string owner, string name, string reference)
        {
            throw new System.NotImplementedException();
        }
    }
}
