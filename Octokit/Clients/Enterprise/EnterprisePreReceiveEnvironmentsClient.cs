using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Pre-receive Environments API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/">Enterprise Pre-receive Environments API documentation</a> for more information.
    ///</remarks>
    public class EnterprisePreReceiveEnvironmentsClient : ApiClient, IEnterprisePreReceiveEnvironmentsClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EnterprisePreReceiveEnvironmentsClient"/>.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EnterprisePreReceiveEnvironmentsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets all <see cref="PreReceiveEnvironment"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#list-pre-receive-environments">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/admin/pre-receive-environments")]
        public Task<IReadOnlyList<PreReceiveEnvironment>> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// Gets all <see cref="PreReceiveEnvironment"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#list-pre-receive-environments">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/admin/pre-receive-environments")]
        public Task<IReadOnlyList<PreReceiveEnvironment>> GetAll(ApiOptions options)
        {
            var endpoint = ApiUrls.AdminPreReceiveEnvironments();
            return ApiConnection.GetAll<PreReceiveEnvironment>(endpoint, null, options);
        }

        /// <summary>
        /// Gets a single <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#get-a-single-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/admin/pre-receive-environments/{pre_receive_environment_id}")]
        public Task<PreReceiveEnvironment> Get(long environmentId)
        {
            var endpoint = ApiUrls.AdminPreReceiveEnvironments(environmentId);
            return ApiConnection.Get<PreReceiveEnvironment>(endpoint, null);
        }

        /// <summary>
        /// Creates a new <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#create-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newPreReceiveEnvironment">A description of the pre-receive environment to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("POST", "/admin/pre-receive-environments")]
        public Task<PreReceiveEnvironment> Create(NewPreReceiveEnvironment newPreReceiveEnvironment)
        {
            Ensure.ArgumentNotNull(newPreReceiveEnvironment, nameof(newPreReceiveEnvironment));

            var endpoint = ApiUrls.AdminPreReceiveEnvironments();
            return ApiConnection.Post<PreReceiveEnvironment>(endpoint, newPreReceiveEnvironment);
        }

        /// <summary>
        /// Edits an existing <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#edit-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <param name="updatePreReceiveEnvironment">A description of the pre-receive environment to edit</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PATCH", "/admin/pre-receive-environments/{pre_receive_environment_id}")]
        public Task<PreReceiveEnvironment> Edit(long environmentId, UpdatePreReceiveEnvironment updatePreReceiveEnvironment)
        {
            Ensure.ArgumentNotNull(updatePreReceiveEnvironment, nameof(updatePreReceiveEnvironment));

            var endpoint = ApiUrls.AdminPreReceiveEnvironments(environmentId);
            return ApiConnection.Patch<PreReceiveEnvironment>(endpoint, updatePreReceiveEnvironment);
        }

        /// <summary>
        /// Deletes an existing <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#delete-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/admin/pre-receive-environments/{pre_receive_environment_id}")]
        public Task Delete(long environmentId)
        {
            var endpoint = ApiUrls.AdminPreReceiveEnvironments(environmentId);
            return ApiConnection.Delete(endpoint, new object());
        }

        /// <summary>
        /// Gets the download status for an existing <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#get-a-pre-receive-environments-download-status">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/admin/pre-receive-environments/{pre_receive_environment_id}/downloads/latest")]
        public Task<PreReceiveEnvironmentDownload> DownloadStatus(long environmentId)
        {
            var endpoint = ApiUrls.AdminPreReceiveEnvironmentDownloadStatus(environmentId);
            return ApiConnection.Get<PreReceiveEnvironmentDownload>(endpoint, null);
        }

        /// <summary>
        /// Triggers a new download of the <see cref="PreReceiveEnvironment"/>'s tarball from the environment's <see cref="PreReceiveEnvironment.ImageUrl"/>.
        /// When the download is finished, the newly downloaded tarball will overwrite the existing environment.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#trigger-a-pre-receive-environment-download">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("POST", "/admin/pre-receive-environments/{pre_receive_environment_id}/downloads")]
        public Task<PreReceiveEnvironmentDownload> TriggerDownload(long environmentId)
        {
            var endpoint = ApiUrls.AdminPreReceiveEnvironmentDownload(environmentId);
            return ApiConnection.Post<PreReceiveEnvironmentDownload>(endpoint, new object());
        }
    }
}
