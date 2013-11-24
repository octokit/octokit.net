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

        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(owner, name));
        }

        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name, string subNamespace)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, "subNamespace");

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(owner, name, subNamespace));
        }

        public Task<Reference> Create(string owner, string name, NewReference reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reference, "reference");

            return ApiConnection.Post<Reference>(ApiUrls.Reference(owner, name), reference);
        }

        public Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNull(referenceUpdate, "update");

            return ApiConnection.Patch<Reference>(ApiUrls.Reference(owner, name, reference), referenceUpdate);
        }

        public Task Delete(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Delete(ApiUrls.Reference(owner, name, reference));
        }
    }
}
