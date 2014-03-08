using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryCommitsClient : IRepositoryCommitsClient
    {
        readonly IApiConnection _apiConnection;

        public RepositoryCommitsClient(IApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public Task<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repositoryName");
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            return _apiConnection.Get<CompareResult>(ApiUrls.RepoCompare(owner, name, @base, head));
        }
    }
}