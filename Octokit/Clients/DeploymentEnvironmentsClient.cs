using Octokit.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        /// Instantiates a new GitHub Repository Environments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DeploymentEnvironmentsClient(IApiConnection apiConnection) : base(apiConnection) { }


        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view environments.
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

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view environments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view environments.
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
            Ensure.ArgumentNotNull(options, nameof(options));

            Dictionary<string, string> prms = null;
            if (options != ApiOptions.None)
            {
                prms = new Dictionary<string, string>();
                prms.Add("per_page", options.PageSize.ToString());
                prms.Add("page", options.StartPage.ToString());
            }

            if (prms != null)
                return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(owner, name), prms);
            else
                return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(owner, name));
        }

        /// <summary>
        /// Gets all the environments for the specified repository. Any user with pull access
        /// to a repository can view environments.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/deployments/environments#list-environments
        /// </remarks>
        /// <param name="repositoryId">Repository ID</param>
        /// <param name="options">Paging options</param>
        [ManualRoute("GET", "/repositories/{id}/environments")]
        public Task<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            Dictionary<string, string> prms = null;
            if (options != ApiOptions.None)
            {
                prms = new Dictionary<string, string>();
                prms.Add("per_page", options.PageSize.ToString());
                prms.Add("page", options.StartPage.ToString());
            }

            if (prms != null)
                return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(repositoryId), prms);
            else
                return ApiConnection.Get<DeploymentEnvironmentsResponse>(ApiUrls.DeploymentEnvironments(repositoryId));
        }
    }
}