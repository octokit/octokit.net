#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Collaborators on a Repository.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details.
    /// </remarks>
    public class RepoCollaboratorsClient : ApiClient, IRepoCollaboratorsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repo Collaborators API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepoCollaboratorsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>.</returns>
        public Task<IReadOnlyList<User>> GetAll(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            var endpoint = "repos/{0}/{1}/collaborators".FormatUri(owner, repo);

            return ApiConnection.GetAll<User>(endpoint);
        }
    }
}
