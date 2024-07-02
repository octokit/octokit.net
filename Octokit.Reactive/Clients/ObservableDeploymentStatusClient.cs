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
            Ensure.ArgumentNotNull(client, nameof(client));

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
        public IObservable<DeploymentStatus> GetAll(string owner, string name, long deploymentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

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
        public IObservable<DeploymentStatus> GetAll(long repositoryId, long deploymentId)
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
        public IObservable<DeploymentStatus> GetAll(string owner, string name, long deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

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
        public IObservable<DeploymentStatus> GetAll(long repositoryId, long deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

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
        public IObservable<DeploymentStatus> Create(string owner, string name, long deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newDeploymentStatus, nameof(newDeploymentStatus));

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
        public IObservable<DeploymentStatus> Create(long repositoryId, long deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNull(newDeploymentStatus, nameof(newDeploymentStatus));

            return _client.Create(repositoryId, deploymentId, newDeploymentStatus).ToObservable();
        }
    }
}
