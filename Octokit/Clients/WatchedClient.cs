using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class WatchedClient : ApiClient, IWatchedClient
    {
         /// <summary>
        /// Instantiates a new GitHub Activity Watching API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public WatchedClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>s watching the passed repository.</returns>
        public Task<IReadOnlyList<User>> GetAllWatchers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<User>(ApiUrls.Watchers(owner, name));
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) watched by the current authenticated user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<Repository>(ApiUrls.Watched());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) watched by the specified user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.GetAll<Repository>(ApiUrls.WatchedByUser(user));
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public async Task<bool> CheckWatched(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            try
            {
                var subscription = await ApiConnection.Get<Subscription>(ApiUrls.Watched(owner, name))
                                                      .ConfigureAwait(false);

                return subscription != null;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Watches a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        /// <returns>A <c>bool</c> representing the success of watching</returns>
        public Task<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newSubscription, "newSubscription");

            return ApiConnection.Put<Subscription>(ApiUrls.Watched(owner, name), newSubscription);
        }

        /// <summary>
        /// Unwatches a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public async Task<bool> UnwatchRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            try
            {
                var statusCode = await Connection.Delete(ApiUrls.Watched(owner, name))
                                                 .ConfigureAwait(false);

                return statusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
