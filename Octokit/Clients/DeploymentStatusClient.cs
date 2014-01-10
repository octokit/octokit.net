using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class DeploymentStatusClient : ApiClient, IDeploymentStatusClient
    {
        const string acceptsHeader = "application/vnd.github.cannonball-preview+json";

        public DeploymentStatusClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<DeploymentStatus>> GetAll(string owner, string name, int deploymentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<DeploymentStatus>(ApiUrls.DeploymentStatuses(owner, name, deploymentId),
                                                          null, acceptsHeader);
        }

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
