using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableWatchedClient
    {
        /// <summary>
        /// Retrieves all of the watchers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s watching the passed repository</returns>
        IObservable<User> GetAllWatchers(string owner, string name);

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> of <see cref="Repository"/></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Repository> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> watched by the specified user</returns>
        IObservable<Repository> GetAllForUser(string user);

        /// <summary>
        /// Check if a repository is watched by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        IObservable<bool> CheckWatched(string owner, string name);

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        IObservable<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription);

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Unwatch",
            Justification = "Unwatch is consistent with the GitHub website")]
        IObservable<bool> UnwatchRepo(string owner, string name);
    }
}