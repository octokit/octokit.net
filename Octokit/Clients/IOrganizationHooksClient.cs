using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IOrganizationHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task<IReadOnlyList<OrganizationHook>> GetAll(string org);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="org"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keywords")]
        Task<OrganizationHook> Get(string org, int hookId);

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task<OrganizationHook> Create(string org, NewOrganizationHook hook);

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook);

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task Ping(string org, int hookId);

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task Delete(string org, int hookId);
    }
}
