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
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Organization.Hooks;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> GetAll(string organizationName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");

            return _connection.GetAndFlattenAllPages<OrganizationHook>(ApiUrls.OrganizationHooks(organizationName));
        }

        /// <summary>
        /// Gets a single hook defined for a organization by id
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Get(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");

            return _client.Get(organizationName, hookId).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Create(string organizationName, NewOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Create(organizationName, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Edit(string organizationName, int hookId, EditOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Edit(organizationName, hookId, hook).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Ping(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");

            return _client.Ping(organizationName, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="organizationName"></param>
        /// <param name="hookId"></param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Delete(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");

            return _client.Delete(organizationName, hookId).ToObservable();
        }
    }
}
