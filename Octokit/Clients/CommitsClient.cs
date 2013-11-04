using System.Threading.Tasks;

namespace Octokit
{
    public class CommitsClient : ApiClient, ICommitsClient
    {
        public CommitsClient(IApiConnection apiConnection) : 
            base(apiConnection)
        {
        }

        public Task<Commit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Commit>(ApiUrls.Commit(owner, name, reference));
        }
    }
}