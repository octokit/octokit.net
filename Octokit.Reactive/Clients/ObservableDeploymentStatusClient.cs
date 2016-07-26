using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive.Clients
{
    /// <summary>
    /// A client for GitHub's Repository Deployment Statuses API.
    /// Gets and creates Deployment Statuses.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/deployments/">Repository Deployment Statuses API documentation</a> for more information.
    /// </remarks>
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
        public IObservable<DeploymentStatus> GetAll(string owner, string name, int deploymentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, deploymentId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the statuses for the given deployment. Any user with pull access to a repository can
        /// view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployment-statuses
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="deploymentId">The id of the deployment.</param>
        public IObservable<DeploymentStatus> GetAll(int repositoryId, int deploymentId)
        {
            return GetAll(repositoryId, deploymentId, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        public IObservable<DeploymentStatus> GetAll(string owner, string name, int deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<DeploymentStatus>(
                ApiUrls.DeploymentStatuses(owner, name, deploymentId), options);
        }

        /// <summary>
        /// Gets all the statuses for the given deployment. Any user with pull access to a repository can
        /// view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployment-statuses
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="deploymentId">The id of the deployment.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<DeploymentStatus> GetAll(int repositoryId, int deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<DeploymentStatus>(
                ApiUrls.DeploymentStatuses(repositoryId, deploymentId), options);
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
        public IObservable<DeploymentStatus> Create(string owner, string name, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeploymentStatus, "newDeploymentStatus");

            return _client.Create(owner, name, deploymentId, newDeploymentStatus).ToObservable();
        }

        /// <summary>
        /// Creates a new status for the given deployment. Users with push access can create deployment
        /// statuses for a given deployment.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment-status
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="deploymentId">The id of the deployment.</param>
        /// <param name="newDeploymentStatus">The new deployment status to create.</param>
        public IObservable<DeploymentStatus> Create(int repositoryId, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNull(newDeploymentStatus, "newDeploymentStatus");

            return _client.Create(repositoryId, deploymentId, newDeploymentStatus).ToObservable();
        }
    }
}
