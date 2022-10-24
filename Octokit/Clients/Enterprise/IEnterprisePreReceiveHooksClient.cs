using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Pre-receive Hooks API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#pre-receive-hooks">Enterprise Pre-receive Hooks API documentation</a> for more information.
    ///</remarks>
    public interface IEnterprisePreReceiveHooksClient
    {
        /// <summary>
        /// Gets all <see cref="PreReceiveHook"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#list-pre-receive-hooks">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<PreReceiveHook>> GetAll();

        /// <summary>
        /// Gets all <see cref="PreReceiveHook"/>s.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#list-pre-receive-hooks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<PreReceiveHook>> GetAll(ApiOptions options);

        /// <summary>
        /// Gets a single <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#get-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="hookId">The id of the pre-receive hook</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="hookId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<PreReceiveHook> Get(long hookId);

        /// <summary>
        /// Creates a new <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#create-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newPreReceiveHook">A description of the pre-receive hook to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<PreReceiveHook> Create(NewPreReceiveHook newPreReceiveHook);

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
        Task<PreReceiveHook> Edit(long hookId, UpdatePreReceiveHook updatePreReceiveHook);

        /// <summary>
        /// Deletes an existing <see cref="PreReceiveHook"/>.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#delete-a-pre-receive-hook">API documentation</a> for more information.
        /// </remarks>
        /// <param name="hookId">The id of the pre-receive hook</param>
        /// <exception cref="NotFoundException">Thrown when the specified <paramref name="hookId"/> does not exist.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(long hookId);
    }
}
