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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keyworks")]
        Task<IReadOnlyList<RepositoryHook>> Get(string owner, string repositoryName);

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repositoryName"></param>
        /// <param name="hookId"></param>
        /// <returns></returns>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        Task<RepositoryHook> GetById(string owner, string repositoryName, int hookId);

        Task<RepositoryHook> Create(string owner, string repositoryName, NewRepositoryHook hook);

        Task<RepositoryHook> Edit(string owner, string repositoryName, string hookId, EditRepositoryHook hook);

        Task Test(string owner, string repositoryName, string hookId);

        Task Delete(string owner, string repositoryName, string hookId);
    }
}
