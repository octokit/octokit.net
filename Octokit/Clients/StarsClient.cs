using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class StarredClient : ApiClient, Octokit.Clients.IStarredClient
    {
        public StarredClient(IApiConnection apiConnection) : base(apiConnection)
        {
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
        /// Retrieves all of the <see cref="Star"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Star"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user)
        {
            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(user));
        }
    }
}
