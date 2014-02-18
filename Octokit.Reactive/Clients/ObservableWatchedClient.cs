using System;
using System.Reactive.Threading.Tasks;

using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWatchedClient : IObservableWatchedClient
    {
        private IWatchedClient _client;
        private IConnection _connection;

        public ObservableWatchedClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Activity.Watching;
            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s watching the passed repository</returns>
        public IObservable<User> GetAllWatchers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Watchers(owner, name));
        }

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> of <see cref="Repository"/></returns>
        public IObservable<Repository> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Watched());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> watched by the specified user</returns>
        public IObservable<Repository> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.WatchedByUser(user));
        }

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public IObservable<bool> CheckWatched(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.CheckWatched(owner, name).ToObservable();
        }

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        public IObservable<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.WatchRepo(owner, name, newSubscription).ToObservable();
        }

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public IObservable<bool> UnwatchRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.UnwatchRepo(owner, name).ToObservable();
        }
    }
}
