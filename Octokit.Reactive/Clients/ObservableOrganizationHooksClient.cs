using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationHooksClient : IObservableOrganizationHooksClient
    {
        readonly IOrganizationHooksClient _client;
        readonly IConnection _connection;

        public ObservableOrganizationHooksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Organization.Hook;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        public IObservable<OrganizationHook> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _connection.GetAndFlattenAllPages<OrganizationHook>(ApiUrls.OrganizationHooks(org));
        }

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        public IObservable<OrganizationHook> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<OrganizationHook>(ApiUrls.OrganizationHooks(org), options);
        }

        /// <summary>
        /// Gets a single hook defined for a organization by id
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        public IObservable<OrganizationHook> Get(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.Get(org, hookId).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<OrganizationHook> Create(string org, NewOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Create(org, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Edit(org, hookId, hook).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Ping(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.Ping(org, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Delete(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.Delete(org, hookId).ToObservable();
        }
    }
}
