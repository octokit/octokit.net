using Octokit.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    public interface IObservableRepositoryDeployEnvironmentsClient
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
        IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, CancellationToken cancellationToken = default);


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
        IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);


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
        IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);
    }
}
