using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Pre-receive Hooks API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#pre-receive-hooks">Enterprise Pre-receive Hooks API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterprisePreReceiveHooksClient : IObservableEnterprisePreReceiveHooksClient
    {
        readonly IEnterprisePreReceiveHooksClient _client;
        readonly IConnection _connection;

        public ObservableEnterprisePreReceiveHooksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Enterprise.PreReceiveHook;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all <see cref="PreReceiveHook"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#list-pre-receive-hooks">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveHook> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// Gets all <see cref="PreReceiveHook"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#list-pre-receive-hooks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveHook> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PreReceiveHook>(ApiUrls.AdminPreReceiveHooks(), null, AcceptHeaders.StableVersionJson, options);
        }

        /// <summary>
        /// Gets a single <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#get-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="hookId">The id of the pre-receive hook</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="hookId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveHook> Get(long hookId)
        {
            return _client.Get(hookId).ToObservable();
        }

        /// <summary>
        /// Creates a new <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#create-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newPreReceiveHook">A description of the pre-receive hook to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveHook> Create(NewPreReceiveHook newPreReceiveHook)
        {
            Ensure.ArgumentNotNull(newPreReceiveHook, nameof(newPreReceiveHook));

            return _client.Create(newPreReceiveHook).ToObservable();
        }

        /// <summary>
        /// Edits an existing <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#update-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="hookId">The id of the pre-receive hook</param>
        /// <param name="updatePreReceiveHook">A description of the pre-receive hook to edit</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="hookId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<PreReceiveHook> Edit(long hookId, UpdatePreReceiveHook updatePreReceiveHook)
        {
            Ensure.ArgumentNotNull(updatePreReceiveHook, nameof(updatePreReceiveHook));

            return _client.Edit(hookId, updatePreReceiveHook).ToObservable();
        }

        /// <summary>
        /// Deletes an existing <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#delete-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="hookId">The id of the pre-receive hook</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="hookId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(long hookId)
        {
            return _client.Delete(hookId).ToObservable();
        }
    }
}
