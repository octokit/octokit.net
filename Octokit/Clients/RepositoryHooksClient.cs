using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Hooks API 
    /// </summary>
    public class RepositoryHooksClient : ApiClient, IRepositoryHooksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryHooksClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<RepositoryHook>> Get(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.GetAll<RepositoryHook>(ApiUrls.RepositoryHooks(owner, repositoryName));
        }

        /// <summary>
        /// Gets a single hook defined for a repository by id
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<RepositoryHook> GetById(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return ApiConnection.Get<RepositoryHook>(ApiUrls.RepositoryHooksById(owner, repositoryName, hookId));
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public Task<RepositoryHook> Create(string owner, string repositoryName, NewRepositoryHook hook)
        {
            throw new System.NotImplementedException();
        }

        public Task<RepositoryHook> Edit(string owner, string repositoryName, string hookId, EditRepositoryHook hook)
        {
            throw new System.NotImplementedException();
        }

        public Task Test(string owner, string repositoryName, string hookId)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string owner, string repositoryName, string hookId)
        {
            throw new System.NotImplementedException();
        }
    }
}