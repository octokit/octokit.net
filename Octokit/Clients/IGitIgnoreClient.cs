using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's gitignore APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/gitignore">GitIgnore API documentation</a> for more details.
    /// </remarks>
    public interface IGitIgnoreClient
    {
        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>A list of template names</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<string>> GetAllGitIgnoreTemplates();


        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns>A template and its source</returns>
        Task<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName);
    }
}