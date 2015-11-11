using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryHooksClient : ApiClient, IRepositoryHooksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryHooksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.GetAll<RepositoryHook>(ApiUrls.RepositoryHooks(owner, repositoryName));
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repositoryName"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        public Task<RepositoryHook> Get(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.Get<RepositoryHook>(ApiUrls.RepositoryHookById(owner, repositoryName, hookId));
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<RepositoryHook> Create(string owner, string repositoryName, NewRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Post<RepositoryHook>(ApiUrls.RepositoryHooks(owner, repositoryName), hook.ToRequest());
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<RepositoryHook> Edit(string owner, string repositoryName, int hookId, EditRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(hook, "hook");

            return ApiConnection.Patch<RepositoryHook>(ApiUrls.RepositoryHookById(owner, repositoryName, hookId), hook);
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        /// <returns></returns>
        public Task Test(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.Post(ApiUrls.RepositoryHookTest(owner, repositoryName, hookId));
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Ping(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.Post(ApiUrls.RepositoryHookPing(owner, repositoryName, hookId));
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task Delete(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.Delete(ApiUrls.RepositoryHookById(owner, repositoryName, hookId));
        }
    }
}