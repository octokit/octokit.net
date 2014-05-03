#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using System.Net;

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

            var endpoint = ApiUrls.RepoCollaborators(owner, repo);
            return ApiConnection.GetAll<User>(endpoint);
        }

        /// <summary>
        /// Checks if a user is a collaborator on a repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#get">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="bool"/>True if user is a collaborator else false</returns>
        public async Task<bool> IsCollaborator(string owner, string repo, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            var endpoint = "repos/{0}/{1}/collaborators/{2}".FormatUri(owner, repo, user);

            try
            {
                var response = await Connection.Get<object>(endpoint, null, null)
                                               .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Task"/></returns>
        public Task Add(string owner, string repo, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            var endpoint = "repos/{0}/{1}/collaborators/{2}".FormatUri(owner, repo, user);

            return ApiConnection.Put(endpoint);
        }

        /// <summary>
        /// Deletes a collaborator from the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Task"/></returns>
        public Task Delete(string owner, string repo, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            var endpoint = "repos/{0}/{1}/collaborators/{2}".FormatUri(owner, repo, user);

            return ApiConnection.Delete(endpoint);
        }
    }
}
