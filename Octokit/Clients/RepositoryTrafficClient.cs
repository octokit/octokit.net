using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryTrafficClient : ApiClient, IRepositoryTrafficClient
    {
        public RepositoryTrafficClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        public Task<IReadOnlyList<RepositoryTrafficPath>> GetPaths(long repositoryId)
        {
            return ApiConnection.GetAll<RepositoryTrafficPath>(ApiUrls.RepositoryTrafficPaths(repositoryId), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<RepositoryTrafficPath>> GetPaths(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<RepositoryTrafficPath>(ApiUrls.RepositoryTrafficPaths(owner, name), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        public Task<IReadOnlyList<RepositoryTrafficReferrer>> GetReferrers(long repositoryId)
        {
            return ApiConnection.GetAll<RepositoryTrafficReferrer>(ApiUrls.RepositoryTrafficReferrers(repositoryId), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<RepositoryTrafficReferrer>> GetReferrers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<RepositoryTrafficReferrer>(ApiUrls.RepositoryTrafficReferrers(owner, name), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public Task<RepositoryTrafficCloneSummary> GetClones(long repositoryId, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNull(per, "per");

            return ApiConnection.Get<RepositoryTrafficCloneSummary>(ApiUrls.RepositoryTrafficClones(repositoryId), per.ToParametersDictionary(), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public Task<RepositoryTrafficCloneSummary> GetClones(string owner, string name, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(per, "per");

            return ApiConnection.Get<RepositoryTrafficCloneSummary>(ApiUrls.RepositoryTrafficClones(owner, name), per.ToParametersDictionary(), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public Task<RepositoryTrafficViewSummary> GetViews(long repositoryId, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNull(per, "per");

            return ApiConnection.Get<RepositoryTrafficViewSummary>(ApiUrls.RepositoryTrafficViews(repositoryId), per.ToParametersDictionary(), AcceptHeaders.RepositoryTrafficApiPreview);
        }

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        public Task<RepositoryTrafficViewSummary> GetViews(string owner, string name, RepositoryTrafficRequest per)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(per, "per");

            return ApiConnection.Get<RepositoryTrafficViewSummary>(ApiUrls.RepositoryTrafficViews(owner, name), per.ToParametersDictionary(), AcceptHeaders.RepositoryTrafficApiPreview);
        }
    }
}
