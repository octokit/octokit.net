using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Webhooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Webhooks API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name);

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<RepositoryHook>> GetAll(long repositoryId);

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<RepositoryHook>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<RepositoryHook>> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keywords")]
        Task<RepositoryHook> Get(string owner, string name, int hookId);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keywords")]
        Task<RepositoryHook> Get(long repositoryId, int hookId);

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> Create(string owner, string name, NewRepositoryHook hook);

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> Create(long repositoryId, NewRepositoryHook hook);

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> Edit(string owner, string name, int hookId, EditRepositoryHook hook);

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> Edit(long repositoryId, int hookId, EditRepositoryHook hook);

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        Task Test(string owner, string name, int hookId);

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        Task Test(long repositoryId, int hookId);

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        Task Ping(string owner, string name, int hookId);

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        Task Ping(long repositoryId, int hookId);

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        Task Delete(string owner, string name, int hookId);

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        Task Delete(long repositoryId, int hookId);
    }
}
