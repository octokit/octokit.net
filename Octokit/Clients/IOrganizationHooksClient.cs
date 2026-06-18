using System.Collections.Generic;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Webhooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/orgs/hooks/">Webhooks API documentation</a> for more information.
    /// </remarks>
    public interface IOrganizationHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<OrganizationHook>> GetAll(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<OrganizationHook>> GetAll(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keywords")]
        Task<OrganizationHook> Get(string org, int hookId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        Task<OrganizationHook> Create(string org, NewOrganizationHook hook, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        Task<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook, CancellationToken cancellationToken = default);

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        Task Ping(string org, int hookId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <param name="org">The organizations name</param>
        /// <param name="hookId">The organizations hook id</param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        Task Delete(string org, int hookId, CancellationToken cancellationToken = default);
    }
}
