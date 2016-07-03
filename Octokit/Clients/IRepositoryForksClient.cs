using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Forks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryForksClient
    {
        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>s representing forks of specified repository.</returns>
        Task<IReadOnlyList<Repository>> GetAll(string owner, string name);

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>s representing forks of specified repository.</returns>
        Task<IReadOnlyList<Repository>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>s representing forks of specified repository.</returns>
        Task<IReadOnlyList<Repository>> GetAll(string owner, string name, RepositoryForksListRequest request);

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>s representing forks of specified repository.</returns>
        Task<IReadOnlyList<Repository>> GetAll(string owner, string name, RepositoryForksListRequest request, ApiOptions options);

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="fork">Used to fork a repository</param>
        /// <returns>A <see cref="Repository"/> representing the created fork of specified repository.</returns>
        Task<Repository> Create(string owner, string name, NewRepositoryFork fork);
    }
}
