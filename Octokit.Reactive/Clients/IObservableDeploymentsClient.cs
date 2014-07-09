using System;

namespace Octokit.Reactive
{
    public interface IObservableDeploymentsClient
    {
        /// <summary>
        /// Gets all the deployments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Deployment"/>s for the specified repository.</returns>
        IObservable<Deployment> GetAll(string owner, string name);

        /// <summary>
        /// Creates a new deployment for the specified repository.
        /// Users with push access can create a deployment for a given ref.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newDeployment">A <see cref="NewDeployment"/> instance describing the new deployment to create</param>
        /// <returns>The created <see cref="Deployment"/></returns>
        IObservable<Deployment> Create(string owner, string name, NewDeployment newDeployment);

        /// <summary>
        /// 
        /// </summary>
        IObservableDeploymentStatusClient Status { get; }
    }
}