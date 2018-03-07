using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Watching API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/activity/watching/">Watching API documentation</a> for more information.
    /// </remarks>
    public class ObservableWatchedClient : IObservableWatchedClient
    {
        private readonly IWatchedClient _client;
        private readonly IConnection _connection;

        public ObservableWatchedClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Activity.Watching;
            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<User> GetAllWatchers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllWatchers(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<User> GetAllWatchers(long repositoryId)
        {
            return GetAllWatchers(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<User> GetAllWatchers(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Watchers(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<User> GetAllWatchers(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Watchers(repositoryId), options);
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<Repository> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<Repository> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Watched(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<Repository> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<Repository> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.WatchedByUser(user), options);
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<bool> CheckWatched(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.CheckWatched(owner, name).ToObservable();
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        public IObservable<bool> CheckWatched(long repositoryId)
        {
            return _client.CheckWatched(repositoryId).ToObservable();
        }

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        public IObservable<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newSubscription, nameof(newSubscription));

            return _client.WatchRepo(owner, name, newSubscription).ToObservable();
        }

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        public IObservable<Subscription> WatchRepo(long repositoryId, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNull(newSubscription, nameof(newSubscription));

            return _client.WatchRepo(repositoryId, newSubscription).ToObservable();
        }

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        public IObservable<bool> UnwatchRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.UnwatchRepo(owner, name).ToObservable();
        }

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<bool> UnwatchRepo(long repositoryId)
        {
            return _client.UnwatchRepo(repositoryId).ToObservable();
        }
    }
}
