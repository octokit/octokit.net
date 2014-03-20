using System.Collections.Generic;
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
        const string acceptsHeader = "application/vnd.github.cannonball-preview+json";

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
        /// <returns>All deployment statuses for the given deployment.</returns>
        public Task<IReadOnlyList<DeploymentStatus>> GetAll(string owner, string name, int deploymentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<DeploymentStatus>(ApiUrls.DeploymentStatuses(owner, name, deploymentId),
                                                          null, acceptsHeader);
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
        /// <param name="newDeploymentStatus"></param>
        /// <returns></returns>
        public Task<DeploymentStatus> Create(string owner, string name, int deploymentId, NewDeploymentStatus newDeploymentStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeploymentStatus, "newDeploymentStatus");

            return ApiConnection.Post<DeploymentStatus>(ApiUrls.DeploymentStatuses(owner, name, deploymentId),
                                                        newDeploymentStatus, acceptsHeader);
        }
    }
}
