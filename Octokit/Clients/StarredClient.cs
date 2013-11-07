using System;
using System.Collections.Generic;
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
            Justification="But i think i do need star-specific request parameters")]
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

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(user));
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

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(user), request.ToParametersDictionary());
        }
    }
}
