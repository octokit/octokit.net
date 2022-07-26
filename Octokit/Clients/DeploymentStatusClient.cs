﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deployment Statuses API.
    /// Gets and creates Deployment Statuses.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/deployments/">Repository Deployment Statuses API documentation</a> for more information.
    /// </remarks>
    public class DeploymentStatusClient : ApiClient, IDeploymentStatusClient
    {
        public DeploymentStatusClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/deployments/{deployment_id}/statuses")]
        public Task<IReadOnlyList<DeploymentStatus>> GetAll(string owner, string name, int deploymentId)
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
        [ManualRoute("GET", "/repositories/{id}/deployments/{deployment_id}/statuses")]
        public Task<IReadOnlyList<DeploymentStatus>> GetAll(long repositoryId, int deploymentId)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/deployments/{deployment_id}/statuses")]
        public Task<IReadOnlyList<DeploymentStatus>> GetAll(string owner, string name, int deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<DeploymentStatus>(ApiUrls.DeploymentStatuses(owner, name, deploymentId), null, options);
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
        [ManualRoute("GET", "/repositories/{id}/deployments/{deployment_id}/statuses")]
        public Task<IReadOnlyList<DeploymentStatus>> GetAll(long repositoryId, int deploymentId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<DeploymentStatus>(ApiUrls.DeploymentStatuses(repositoryId, deploymentId), null, options);
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/deployments/{deployment_id}/statuses")]
        public Task<DeploymentStatus> Create(string owner, string name, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newDeploymentStatus, nameof(newDeploymentStatus));

            return ApiConnection.Post<DeploymentStatus>(ApiUrls.DeploymentStatuses(owner, name, deploymentId), newDeploymentStatus);
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
        [ManualRoute("POST", "/repositories/{id}/deployments/{deployment_id}/statuses")]
        public Task<DeploymentStatus> Create(long repositoryId, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNull(newDeploymentStatus, nameof(newDeploymentStatus));

            return ApiConnection.Post<DeploymentStatus>(ApiUrls.DeploymentStatuses(repositoryId, deploymentId), newDeploymentStatus);
        }
    }
}
