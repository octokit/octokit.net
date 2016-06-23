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
        public Task<IReadOnlyList<Deployment>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the deployments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<Deployment>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<Deployment>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Deployment>(ApiUrls.Deployments(owner, name),
                                                    null,
                                                    AcceptHeaders.DeploymentApiPreview,
                                                    options);
        }

        /// <summary>
        /// Gets all the deployments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<Deployment>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Deployment>(ApiUrls.Deployments(repositoryId), options);
        }

        /// <summary>
        /// Creates a new deployment for the specified repository.
        /// Users with push access can create a deployment for a given ref.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newDeployment">A <see cref="NewDeployment"/> instance describing the new deployment to create</param>
        public Task<Deployment> Create(string owner, string name, NewDeployment newDeployment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeployment, "newDeployment");

            return ApiConnection.Post<Deployment>(ApiUrls.Deployments(owner, name),
                                                  newDeployment,
                                                  AcceptHeaders.DeploymentApiPreview);
        }

        /// <summary>
        /// Creates a new deployment for the specified repository.
        /// Users with push access can create a deployment for a given ref.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newDeployment">A <see cref="NewDeployment"/> instance describing the new deployment to create</param>
        public Task<Deployment> Create(long repositoryId, NewDeployment newDeployment)
        {
            Ensure.ArgumentNotNull(newDeployment, "newDeployment");

            return ApiConnection.Post<Deployment>(ApiUrls.Deployments(repositoryId),
                                                     newDeployment);
        }

        /// <summary>
        /// Client for managing deployment status.
        /// </summary>
        public IDeploymentStatusClient Status { get; set; }
    }
}
