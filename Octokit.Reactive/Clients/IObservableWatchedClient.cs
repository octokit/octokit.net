using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Watching API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/activity/watching/">Watching API documentation</a> for more information.
    /// </remarks>
    public interface IObservableWatchedClient
    {
        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<User> GetAllWatchers(string owner, string name);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<User> GetAllWatchers(long repositoryId);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<User> GetAllWatchers(string owner, string name, ApiOptions options);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<User> GetAllWatchers(long repositoryId, ApiOptions options);

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Repository> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<Repository> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<Repository> GetAllForUser(string user);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<Repository> GetAllForUser(string user, ApiOptions options);

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<bool> CheckWatched(string owner, string name);

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        IObservable<bool> CheckWatched(long repositoryId);

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        IObservable<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription);

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        IObservable<Subscription> WatchRepo(long repositoryId, NewSubscription newSubscription);

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unwatch",
            Justification = "Unwatch is consistent with the GitHub website")]
        IObservable<bool> UnwatchRepo(string owner, string name);

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unwatch",
            Justification = "Unwatch is consistent with the GitHub website")]
        IObservable<bool> UnwatchRepo(long repositoryId);
    }
}