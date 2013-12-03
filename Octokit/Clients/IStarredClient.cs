using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Starring API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/starring/">Activity Starring API documentation</a> for more information.
    /// </remarks>
    public interface IStarredClient
    {
        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>s starring the passed repository.</returns>
        Task<IReadOnlyList<User>> GetAllStargazers(string owner, string name);

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user.
        /// </returns>
        Task<IReadOnlyList<Repository>> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters.
        /// </returns>
        Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) starred by the specified user.
        /// </returns>
        Task<IReadOnlyList<Repository>> GetAllForUser(string user);

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> starred by the specified user.</returns>
        Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request);

        /// <summary>
        /// Check if a repository is starred by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        Task<bool> CheckStarred(string owner, string name);

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        Task<bool> StarRepo(string owner, string name);

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        Task<bool> RemoveStarFromRepo(string owner, string name);
    }
}
