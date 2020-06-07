using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Watching API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/activity/watching/">Watching API documentation</a> for more information.
    /// </remarks>
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/subscribers")]
        public Task<IReadOnlyList<User>> GetAllWatchers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllWatchers(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/subscribers")]
        public Task<IReadOnlyList<User>> GetAllWatchers(long repositoryId)
        {
            return GetAllWatchers(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/subscribers")]
        public Task<IReadOnlyList<User>> GetAllWatchers(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Watchers(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/subscribers")]
        public Task<IReadOnlyList<User>> GetAllWatchers(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Watchers(repositoryId), options);
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) watched by the current authenticated user.
        /// </returns>
        [ManualRoute("GET", "/user/subscribers")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) watched by the current authenticated user.
        /// </returns>
        [ManualRoute("GET", "/user/subscribers")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Watched(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) watched by the specified user.
        /// </returns>
        [ManualRoute("GET", "/users/{username}/subscriptions")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) watched by the specified user.
        /// </returns>
        [ManualRoute("GET", "/users/{username}/subscriptions")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.WatchedByUser(user), options);
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/subscription")]
        public async Task<bool> CheckWatched(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var endpoint = ApiUrls.Watched(owner, name);
                var subscription = await ApiConnection.Get<Subscription>(endpoint).ConfigureAwait(false);

                return subscription != null;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/subscription")]
        public async Task<bool> CheckWatched(long repositoryId)
        {
            try
            {
                var endpoint = ApiUrls.Watched(repositoryId);
                var subscription = await ApiConnection.Get<Subscription>(endpoint).ConfigureAwait(false);

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
        [ManualRoute("PUT", "/repos/{owner}/{repo}/subscription")]
        public Task<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newSubscription, nameof(newSubscription));

            return ApiConnection.Put<Subscription>(ApiUrls.Watched(owner, name), newSubscription);
        }

        /// <summary>
        /// Watches a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        [ManualRoute("PUT", "/repositories/{id}/subscription")]
        public Task<Subscription> WatchRepo(long repositoryId, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNull(newSubscription, nameof(newSubscription));

            return ApiConnection.Put<Subscription>(ApiUrls.Watched(repositoryId), newSubscription);
        }

        /// <summary>
        /// Unwatches a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/subscription")]
        public async Task<bool> UnwatchRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var endpoint = ApiUrls.Watched(owner, name);
                var statusCode = await Connection.Delete(endpoint).ConfigureAwait(false);

                return statusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Unwatches a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("DELETE", "/repositories/{id}/subscription")]
        public async Task<bool> UnwatchRepo(long repositoryId)
        {
            try
            {
                var endpoint = ApiUrls.Watched(repositoryId);
                var statusCode = await Connection.Delete(endpoint).ConfigureAwait(false);

                return statusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
