using Octokit.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deployment Environments API.
    /// Gets Deployment Environments.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/deployments/environments">Repository Deployment Environments API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryDeployEnvironmentsClient
    {
        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<DeploymentEnvironmentsResponse> GetAll(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId, CancellationToken cancellationToken = default);


        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Paging options</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);


        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">Repository ID</param>
        /// <param name="options">Paging options</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);
    }
}
