using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Hooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Repository Hooks API documentation</a> for more information.
    /// </remarks>
    public class RepositoryHooksClient : ApiClient, IRepositoryHooksClient
    {
        /// <summary>
        /// Initializes a new API client.
        /// </summary>
        /// <param name="apiConnection">The client's connection</param>
        public RepositoryHooksClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all registered hooks for the specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#list-hooks</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Hook>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Hook>(ApiUrls.Hooks(owner, name));
        }

        /// <summary>
        /// Gets a single hook for the specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#get-single-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook identifier.</param>
        /// <returns></returns>
        public Task<Hook> Get(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Hook>(ApiUrls.Hook(owner, name, hookId));
        }

        /// <summary>
        /// Creates a new Hook for a specified repository.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#create-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newHook">The new hook</param>
        /// <returns></returns>
        public Task<Hook> Create(string owner, string name, NewHook newHook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newHook, "newHook");

            return ApiConnection.Post<Hook>(ApiUrls.Hooks(owner, name), newHook);
        }

        /// <summary>
        /// Updates a specified Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#edit-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook identifier.</param>
        /// <param name="hookUpdate">The hook update.</param>
        /// <returns></returns>
        public Task<Hook> Update(string owner, string name, int hookId, HookUpdate hookUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(hookUpdate, "hookUpdate");

            return ApiConnection.Patch<Hook>(ApiUrls.Hook(owner, name, hookId), hookUpdate);
        }

        /// <summary>
        /// Pings a specified Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#ping-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        public Task<Hook> Ping(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            
            return ApiConnection.Post<Hook>(ApiUrls.Hook(owner, name, hookId, "pings"), null);
        }

        /// <summary>
        /// Tests a push Hook.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#test-a-push-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        public Task<Hook> TestPush(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Post<Hook>(ApiUrls.Hook(owner, name, hookId, "tests"), null);
        }

        /// <summary>
        /// Deletes the specified Hook
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/hooks/#delete-a-hook</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="hookId">The hook id</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.Hook(owner, name, hookId));
        }
    }
}