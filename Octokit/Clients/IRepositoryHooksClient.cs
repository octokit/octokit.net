using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IRepositoryHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#json-http">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        Task<IReadOnlyList<RepositoryHook>> GetHooks(string owner, string repositoryName);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repositoryName"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> GetHookById(string owner, string repositoryName, int hookId);

        Task<RepositoryHook> CreateHook(string owner, string repositoryName, NewRepositoryHook hook);

        Task<RepositoryHook> EditHook(string owner, string repositoryName, string hookId, EditRepositoryHook hook);

        Task TestHook(string owner, string repositoryName, string hookId);

        Task DeleteHook(string owner, string repositoryName, string hookId);
    }
}
