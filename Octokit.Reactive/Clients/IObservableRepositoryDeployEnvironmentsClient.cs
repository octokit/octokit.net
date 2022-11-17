using Octokit.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
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
        IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name);

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId);


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
        IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options);


        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">Repository ID</param>
        /// <param name="options">Paging options</param>
        IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options);
    }
}
