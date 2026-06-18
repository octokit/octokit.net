using System;

using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Activity Starring API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/starring/">Activity Starring API documentation</a> for more information.
    /// </remarks>
    public interface IObservableStarredClient
    {
        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<User> GetAllStargazers(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<User> GetAllStargazers(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<User> GetAllStargazers(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<User> GetAllStargazers(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<UserStar> GetAllStargazersWithTimestamps(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<UserStar> GetAllStargazersWithTimestamps(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<UserStar> GetAllStargazersWithTimestamps(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<UserStar> GetAllStargazersWithTimestamps(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForCurrent(StarredRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForCurrent(StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(StarredRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForUser(string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForUser(string user, StarredRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Repository> GetAllForUser(string user, StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, StarredRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if a repository is starred by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> CheckStarred(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Stars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> StarRepo(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unstars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> RemoveStarFromRepo(string owner, string name, CancellationToken cancellationToken = default);
    }
}
