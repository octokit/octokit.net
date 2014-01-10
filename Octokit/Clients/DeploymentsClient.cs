using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class DeploymentsClient : ApiClient, IDeploymentsClient
    {
        const string acceptsHeader = "application/vnd.github.cannonball-preview+json";

        public DeploymentsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<GitDeployment>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "login");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<GitDeployment>(ApiUrls.Deployments(owner, name),
                                                       null, acceptsHeader);
        }

        public Task<GitDeployment> Create(string owner, string name, NewDeployment newDeployment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeployment, "deployment");

            return ApiConnection.Post<GitDeployment>(ApiUrls.Deployments(owner, name),
                                                     newDeployment, acceptsHeader);
        }
    }
}
