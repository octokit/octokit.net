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

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <returns></returns>
        public Task<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");

            return _apiConnection.Get<CompareResult>(ApiUrls.RepoCompare(owner, name, @base, head));
        }
    }
}