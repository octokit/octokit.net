using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive.Clients
{
    public class ObservableDeploymentStatusClient : IObservableDeploymentStatusClient
    {
        private readonly IDeploymentStatusClient _client;
        private readonly IConnection _connection;

        public ObservableDeploymentStatusClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Deployment.Status;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the statuses for the given deployment. Any user with pull access to a repository can
        /// view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployment-statuses
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="deploymentId">The id of the deployment.</param>
        /// <returns>All deployment statuses for the given deployment.</returns>
        public IObservable<DeploymentStatus> GetAll(string owner, string name, int deploymentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<DeploymentStatus>(
                ApiUrls.DeploymentStatuses(owner, name, deploymentId));
        }

        /// <summary>
        /// Creates a new status for the given deployment. Users with push access can create deployment
        /// statuses for a given deployment.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment-status
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="deploymentId">The id of the deployment.</param>
        /// <param name="newDeploymentStatus">The new deployment status to create.</param>
        /// <returns></returns>
        public IObservable<DeploymentStatus> Create(string owner, string name, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            return _client.Create(owner, name, deploymentId, newDeploymentStatus).ToObservable();
        }
    }
}
