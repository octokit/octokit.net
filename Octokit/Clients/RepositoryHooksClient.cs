using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Webhooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Webhooks API documentation</a> for more information.
    /// </remarks>
    public class RepositoryHooksClient : ApiClient, IRepositoryHooksClient
    {
        /// <summary>
        /// Initializes a new GitHub Webhooks API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryHooksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/hooks")]
        public Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repositories/{id}/hooks")]
        public Task<IReadOnlyList<RepositoryHook>> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/hooks")]
        public Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryHook>(ApiUrls.RepositoryHooks(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repositories/{id}/hooks")]
        public Task<IReadOnlyList<RepositoryHook>> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryHook>(ApiUrls.RepositoryHooks(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/hooks/{id}")]
        public Task<RepositoryHook> Get(string owner, string name, int hookId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<RepositoryHook>(ApiUrls.RepositoryHookById(owner, name, hookId), null, null, cancellationToken);
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("GET", "/repositories/{id}/hooks/{id}")]
        public Task<RepositoryHook> Get(long repositoryId, int hookId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<RepositoryHook>(ApiUrls.RepositoryHookById(repositoryId, hookId), null, null, cancellationToken);
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("POST", "/repos/{owner}/{repo}/hooks")]
        public Task<RepositoryHook> Create(string owner, string name, NewRepositoryHook hook, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Post<RepositoryHook>(ApiUrls.RepositoryHooks(owner, name), hook.ToRequest(), cancellationToken);
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("POST", "/repositories/{id}/hooks")]
        public Task<RepositoryHook> Create(long repositoryId, NewRepositoryHook hook, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Post<RepositoryHook>(ApiUrls.RepositoryHooks(repositoryId), hook.ToRequest(), cancellationToken);
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/hooks/{id}")]
        public Task<RepositoryHook> Edit(string owner, string name, int hookId, EditRepositoryHook hook, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Patch<RepositoryHook>(ApiUrls.RepositoryHookById(owner, name, hookId), hook, cancellationToken);
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("POST", "/repositories/{id}/hooks/{hook_id}")]
        public Task<RepositoryHook> Edit(long repositoryId, int hookId, EditRepositoryHook hook, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return ApiConnection.Patch<RepositoryHook>(ApiUrls.RepositoryHookById(repositoryId, hookId), hook, cancellationToken);
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information.
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        [ManualRoute("POST", "/repos/{owner}/{repo}/hooks/{id}/tests")]
        public Task Test(string owner, string name, int hookId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.RepositoryHookTest(owner, name, hookId), cancellationToken);
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information.
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        [ManualRoute("POST", "/repositories/{id}/hooks/{hook_id}/tests")]
        public Task Test(long repositoryId, int hookId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Post(ApiUrls.RepositoryHookTest(repositoryId, hookId), cancellationToken);
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("POST", "/repos/{owner}/{repo}/hooks/{id}/pings")]
        public Task Ping(string owner, string name, int hookId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.RepositoryHookPing(owner, name, hookId), cancellationToken);
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("POST", "/repositories/{id}/hooks/{hook_id}/pings")]
        public Task Ping(long repositoryId, int hookId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Post(ApiUrls.RepositoryHookPing(repositoryId, hookId), cancellationToken);
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/hooks/{id}")]
        public Task Delete(string owner, string name, int hookId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.RepositoryHookById(owner, name, hookId), cancellationToken);
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        [ManualRoute("DELETE", "/repositories/{id}/hooks/{hook_id}")]
        public Task Delete(long repositoryId, int hookId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.RepositoryHookById(repositoryId, hookId), cancellationToken);
        }
    }
}
