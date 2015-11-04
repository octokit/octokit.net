using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deploy Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/keys/">Deploy Keys API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryDeployKeysClient
    {
        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#get"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key.</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<DeployKey> Get(string owner, string name, int number);

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        Task<IReadOnlyList<DeployKey>> GetAll(string owner, string name);

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#create"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        /// <returns></returns>
        Task<DeployKey> Create(string owner, string name, NewDeployKey newDeployKey);

        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#delete"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key to delete.</param>
        /// <returns></returns>
        Task Delete(string owner, string name, int number);
    }
}
