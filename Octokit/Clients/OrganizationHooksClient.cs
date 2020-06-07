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
        /// <param name="org">The organization's name</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("GET", "orgs/{org}/hooks")]
        public Task<IReadOnlyList<OrganizationHook>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.GetAll<OrganizationHook>(ApiUrls.OrganizationHooks(org));
        }

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <param name="org">The organization's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("GET", "orgs/{org}/hooks")]
        public Task<IReadOnlyList<OrganizationHook>> GetAll(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<OrganizationHook>(ApiUrls.OrganizationHooks(org), options);
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="org"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "orgs/{org}/hooks/{hook_id}")]
        public Task<OrganizationHook> Get(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hookId, nameof(hookId));

            return ApiConnection.Get<OrganizationHook>(ApiUrls.OrganizationHookById(org, hookId));
        }

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("POST", "orgs/{org}/hooks")]
        public Task<OrganizationHook> Create(string org, NewOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Post<OrganizationHook>(ApiUrls.OrganizationHooks(org), hook.ToRequest());
        }

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("PATCH", "orgs/{org}/hooks/{hook_id}")]
        public Task<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Patch<OrganizationHook>(ApiUrls.OrganizationHookById(org, hookId), hook);
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("POST", "orgs/{org}/hooks/{hook_id}/pings")]
        public Task Ping(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hookId, nameof(hookId));
            return ApiConnection.Post(ApiUrls.OrganizationHookPing(org, hookId));
        }

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [ManualRoute("DELETE", "orgs/{org}/hooks/{hook_id}")]
        public Task Delete(string org, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(hookId, nameof(hookId));

            return ApiConnection.Delete(ApiUrls.OrganizationHookById(org, hookId));
        }
    }
}
