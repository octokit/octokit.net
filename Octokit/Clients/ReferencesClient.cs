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
    }
}
