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
    public interface IEnterprisePreReceiveEnvironmentsClient
    {
        /// <summary>
        /// Gets all <see cref="PreReceiveEnvironment"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#list-pre-receive-environments">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<PreReceiveEnvironment>> GetAll();

        /// <summary>
        /// Gets all <see cref="PreReceiveEnvironment"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#list-pre-receive-environments">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<PreReceiveEnvironment>> GetAll(ApiOptions options);

        /// <summary>
        /// Gets a single <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#get-a-single-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<PreReceiveEnvironment> Get(long environmentId);

        /// <summary>
        /// Creates a new <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#create-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newPreReceiveEnvironment">A description of the pre-receive environment to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<PreReceiveEnvironment> Create(NewPreReceiveEnvironment newPreReceiveEnvironment);

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
        Task<PreReceiveEnvironment> Edit(long environmentId, UpdatePreReceiveEnvironment updatePreReceiveEnvironment);

        /// <summary>
        /// Deletes an existing <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#delete-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(long environmentId);

        /// <summary>
        /// Gets the download status for an existing <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#get-a-pre-receive-environments-download-status">API documentation</a> for more information.
        /// </remarks>
        /// <param name="environmentId">The id of the pre-receive environment</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="environmentId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<PreReceiveEnvironmentDownload> DownloadStatus(long environmentId);

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
        Task<PreReceiveEnvironmentDownload> TriggerDownload(long environmentId);
    }
}
