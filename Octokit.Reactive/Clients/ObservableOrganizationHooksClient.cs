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
        public IObservable<OrganizationHook> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            

            return _connection.GetAndFlattenAllPages<OrganizationHook>(ApiUrls.OrganizationHooks(org));
        }

        /// <summary>
        /// Gets a single hook defined for a organization by id
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Get(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return _client.Get(org, hookId).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Create(string org, NewOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Create(org, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Edit(org, hookId, hook).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Ping(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return _client.Ping(org, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="org"></param>
        /// <param name="hookId"></param>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Delete(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return _client.Delete(org, hookId).ToObservable();
        }
    }
}
