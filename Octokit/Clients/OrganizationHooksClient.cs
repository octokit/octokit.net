using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class OrganizationHooksClient : ApiClient, IOrganizationHooksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public OrganizationHooksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<OrganizationHook>> GetAll(string organizationName)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "OrganizationName");

            return ApiConnection.GetAll<OrganizationHook>(ApiUrls.OrganizationHooks(organizationName));
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="organizationName"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        public Task<OrganizationHook> Get(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(hookId, "HookId");

            return ApiConnection.Get<OrganizationHook>(ApiUrls.OrganizationHookById(organizationName, hookId));
        }

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<OrganizationHook> Create(string organizationName, NewOrganizationHook hook)
        { 
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Post<OrganizationHook>(ApiUrls.OrganizationHooks(organizationName), hook.ToRequest());
        }

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<OrganizationHook> Edit(string organizationName, int hookId, EditOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "OrganizationName");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Patch<OrganizationHook>(ApiUrls.OrganizationHookById(organizationName, hookId), hook);
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Ping(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "OrganizationName");
            Ensure.ArgumentNotNull(hookId, "hookId");
            return ApiConnection.Post(ApiUrls.OrganizationHookPing(organizationName, hookId));
        }

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Delete(string organizationName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(hookId, "hookId");

            return ApiConnection.Delete(ApiUrls.OrganizationHookById(organizationName, hookId));
        }
    }
}