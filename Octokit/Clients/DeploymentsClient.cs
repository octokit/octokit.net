using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deployments API.
    /// Gets and creates Deployments.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/deployments/">Repository Deployments API documentation</a> for more information.
    /// </remarks>
    public class DeploymentsClient : ApiClient, IDeploymentsClient
    {
        const string acceptsHeader = "application/vnd.github.cannonball-preview+json";

        /// <summary>
        /// Instantiates a new GitHub Repository Deployments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DeploymentsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Status = new DeploymentStatusClient(apiConnection);
        }

        /// <summary>
        /// Gets all the deployments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="GitDeployment"/>s for the specified repository.</returns>
        public Task<IReadOnlyList<GitDeployment>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "login");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<GitDeployment>(ApiUrls.Deployments(owner, name),
                                                       null, acceptsHeader);
        }

        /// <summary>
        /// Creates a new deployment for the specified repository.
        /// Users with push access can create a deployment for a given ref.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newDeployment">A <see cref="NewDeployment"/> instance describing the new deployment to create</param>
        /// <returns>The created <see cref="GitDeployment"></returns>
        public Task<GitDeployment> Create(string owner, string name, NewDeployment newDeployment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeployment, "deployment");

            return ApiConnection.Post<GitDeployment>(ApiUrls.Deployments(owner, name),
                                                     newDeployment, acceptsHeader);
        }

        /// <summary>
        /// Client for managing deployment status.
        /// </summary>
        public IDeploymentStatusClient Status { get; set; }
    }
}
