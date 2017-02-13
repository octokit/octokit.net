using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Watching API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/activity/watching/">Watching API documentation</a> for more information.
    /// </remarks>
    public interface IWatchedClient
    {
        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<User>> GetAllWatchers(string owner, string name);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<User>> GetAllWatchers(long repositoryId);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<User>> GetAllWatchers(string owner, string name, ApiOptions options);

        /// <summary>
        /// Retrieves all of the watchers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<User>> GetAllWatchers(long repositoryId, ApiOptions options);

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) watched by the current authenticated user.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Repository>> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the watched <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) watched by the current authenticated user.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) watched by the specified user.
        /// </returns>
        Task<IReadOnlyList<Repository>> GetAllForUser(string user);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) watched by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing API's response.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) watched by the specified user.
        /// </returns>
        Task<IReadOnlyList<Repository>> GetAllForUser(string user, ApiOptions options);

        /// <summary>
        /// Check if a repository is watched by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<bool> CheckWatched(string owner, string name);

        /// <summary>
        /// Check if a repository is watched by the current authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<bool> CheckWatched(long repositoryId);

        /// <summary>
        /// Watches a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        Task<Subscription> WatchRepo(string owner, string name, NewSubscription newSubscription);

        /// <summary>
        /// Watches a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newSubscription">A <see cref="NewSubscription"/> instance describing the new subscription to create</param>
        Task<Subscription> WatchRepo(long repositoryId, NewSubscription newSubscription);

        /// <summary>
        /// Unwatches a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unwatch",
            Justification = "Unwatch is consistent with the GitHub website")]
        Task<bool> UnwatchRepo(string owner, string name);

        /// <summary>
        /// Unwatches a repository for the authenticated user.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unwatch",
            Justification = "Unwatch is consistent with the GitHub website")]
        Task<bool> UnwatchRepo(long repositoryId);
    }
}