using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IDeploymentStatusClient
    {
        Task<IReadOnlyList<DeploymentStatus>> GetAll(string owner, string name, int deploymentId);

        Task<DeploymentStatus> Create(string owner, string name, int deploymentId, NewDeploymentStatus newDeploymentStatus);
    }
}