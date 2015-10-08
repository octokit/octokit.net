using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryForksClient : ApiClient, IRepositoryForksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos Fork API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryForksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<Repository>> GetAll(string owner, string repositoryName, RepositoryForksListRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return request == null
                ? ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(owner, repositoryName))
                : ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(owner, repositoryName), request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<Repository> Create(string owner, string repositoryName, NewRepositoryFork fork)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(fork, "fork");

            return ApiConnection.Post<Repository>(ApiUrls.RepositoryForks(owner, repositoryName), fork);
        }
    }
}