using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IDeploymentsClient
    {
        Task<IReadOnlyList<GitDeployment>> GetAll(string owner, string name);
        Task<GitDeployment> Create(string owner, string name, NewDeployment newDeployment);
        IDeploymentStatusClient Status { get; }
    }
}