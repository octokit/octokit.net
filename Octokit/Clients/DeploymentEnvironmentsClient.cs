using Octokit.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deployment Environments API.
    /// Gets Deployment Environments.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/deployments/environments">Repository Deployment Environments API documentation</a> for more information.
    /// </remarks>
    public class DeploymentEnvironmentsClient : ApiClient, IRepositoryDeployEnvironmentsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Repository Deployments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DeploymentEnvironmentsClient(IApiConnection apiConnection) : base(apiConnection) { }


        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, null);
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, null);
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Paging options</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            Dictionary<string, string> prms = null;
            if (options != null)
            {
                prms = new Dictionary<string, string>();
                prms.Add("per_page", options.PageSize.ToString());
                prms.Add("page", options.StartPage.ToString());
            }

            return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(owner, name), prms);
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">Repository ID</param>
        /// <param name="options">Paging options</param>
        [ManualRoute("GET", "/repositories/{id}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Dictionary<string, string> prms = null;
            if (options != null)
            {
                prms = new Dictionary<string, string>();
                prms.Add("per_page", options.PageSize.ToString());
                prms.Add("page", options.StartPage.ToString());
            }

            return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(repositoryId), prms);
        }
    }
}
