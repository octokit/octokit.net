using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class StarredClient : ApiClient, IStarredClient
    {
        public StarredClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>.</returns>
        public Task<IReadOnlyList<User>> GetAllStargazers(string owner, string repo)
        {
            return ApiConnection.GetAll<User>(ApiUrls.Stargazers(owner, repo));
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<Repository>(ApiUrls.Starred());
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the resulting stars</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "But i think i do need star-specific request parameters")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> starred by the specified user.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user));
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> starred by the specified user.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user), request.ToParametersDictionary());
        }

        /// <summary>
        /// Check if a repository is starred by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        public async Task<bool> CheckStarred(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNull(repo, "repo");

            try
            {
                var response = await Connection.GetAsync<object>(ApiUrls.Starred(owner, repo), null, null)
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

        public async Task<bool> StarRepo(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNull(repo, "repo");

            try
            {
                var response = await Connection.PutAsync<object>(ApiUrls.Starred(owner, repo), null, null)
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
    }
}
