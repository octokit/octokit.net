using System;

namespace Octokit.Reactive
{
    public interface IObservableStarredClient
    {
        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s starring the passed repository</returns>
        IObservable<User> GetAllStargazers(string owner, string name);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user
        /// </returns>
        IObservable<Repository> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters
        /// </returns>
        IObservable<Repository> GetAllForCurrent(StarredRequest request);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        IObservable<Repository> GetAllForUser(string user);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        IObservable<Repository> GetAllForUser(string user, StarredRequest request);

        /// <summary>
        /// Check if a repository is starred by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        IObservable<bool> CheckStarred(string owner, string name);

        /// <summary>
        /// Stars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        IObservable<bool> StarRepo(string owner, string name);

        /// <summary>
        /// Unstars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        IObservable<bool> RemoveStarFromRepo(string owner, string name);
    }
}
