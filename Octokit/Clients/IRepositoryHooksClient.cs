using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Hooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Repository Hooks API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryHooksClient
    {
        /// <summary>
        /// Gets all registered hooks for the specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#list-hooks</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<Hook>> GetAll(string owner, string name);

        /// <summary>
        /// Gets a single hook for the specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#get-single-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook identifier.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Hook> Get(string owner, string name, int hookId);

        /// <summary>
        /// Creates a new Hook for a specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#create-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newHook">The new hook</param>
        /// <returns></returns>
        Task<Hook> Create(string owner, string name, NewHook newHook);

        /// <summary>
        /// Updates a specified Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#edit-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook identifier.</param>
        /// <param name="hookUpdate">The hook update.</param>
        /// <returns></returns>
        Task<Hook> Update(string owner, string name, int hookId, HookUpdate hookUpdate);

        /// <summary>
        /// Tests a push Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#test-a-push-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        Task<Hook> TestPush(string owner, string name, int hookId);

        /// <summary>
        /// Pings a specified Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#ping-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        Task<Hook> Ping(string owner, string name, int hookId);

        /// <summary>
        /// Deletes the specified Hook
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#delete-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        Task Delete(string owner, string name, int hookId);
    }
}
