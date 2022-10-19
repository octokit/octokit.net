using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Pre-receive Environments API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/">Enterprise Pre-receive Environments API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterprisePreReceiveEnvironmentsClient : IObservableEnterprisePreReceiveEnvironmentsClient
    {
        readonly IEnterprisePreReceiveEnvironmentsClient _client;
        readonly IConnection _connection;

        public ObservableEnterprisePreReceiveEnvironmentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Enterprise.PreReceiveEnvironment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all <see cref="PreReceiveEnvironment"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#list-pre-receive-environments">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveEnvironment> GetAll()
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
        public IObservable<PreReceiveEnvironment> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PreReceiveEnvironment>(ApiUrls.AdminPreReceiveEnvironments(), null, options);
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
        public IObservable<PreReceiveEnvironment> Get(long environmentId)
        {
            return _client.Get(environmentId).ToObservable();
        }

        /// <summary>
        /// Creates a new <see cref="PreReceiveEnvironment"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/#create-a-pre-receive-environment">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newPreReceiveEnvironment">A description of the pre-receive environment to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveEnvironment> Create(NewPreReceiveEnvironment newPreReceiveEnvironment)
        {
            Ensure.ArgumentNotNull(newPreReceiveEnvironment, nameof(newPreReceiveEnvironment));

            return _client.Create(newPreReceiveEnvironment).ToObservable();
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
        public IObservable<PreReceiveEnvironment> Edit(long environmentId, UpdatePreReceiveEnvironment updatePreReceiveEnvironment)
        {
            Ensure.ArgumentNotNull(updatePreReceiveEnvironment, nameof(updatePreReceiveEnvironment));

            return _client.Edit(environmentId, updatePreReceiveEnvironment).ToObservable();
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
        public IObservable<Unit> Delete(long environmentId)
        {
            return _client.Delete(environmentId).ToObservable();
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
        public IObservable<PreReceiveEnvironmentDownload> DownloadStatus(long environmentId)
        {
            return _client.DownloadStatus(environmentId).ToObservable();
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
        public IObservable<PreReceiveEnvironmentDownload> TriggerDownload(long environmentId)
        {
            return _client.TriggerDownload(environmentId).ToObservable();
        }
    }
}
