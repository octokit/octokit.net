
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Autolinks API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/repos/autolinks">API documentation</a> for more information.
    /// </remarks>
    public interface IAutolinksClient
    {
        /// <summary>
        /// Returns a single autolink reference by ID that was configured for the given repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="autolinkId">The unique identifier of the autolink</param>
        /// <remarks>See the <a href="https://docs.github.com/en/rest/repos/autolinks#get-an-autolink-reference-of-a-repository">API documentation</a> for more information.</remarks>
        Task<Autolink> Get(string owner, string repo, int autolinkId);

        /// <summary>
        /// Returns a list of autolinks configured for the given repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <remarks>See the <a href="https://docs.github.com/en/rest/repos/autolinks#list-all-autolinks-of-a-repository">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<Autolink>> GetAll(string owner, string repo);

        /// <summary>
        /// Returns a list of autolinks configured for the given repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See the <a href="https://docs.github.com/en/rest/repos/autolinks#list-all-autolinks-of-a-repository">API documentation</a> for more information.</remarks>
        Task<IReadOnlyList<Autolink>> GetAll(string owner, string repo, ApiOptions options);

        /// <summary>
        /// Create an autolink reference for a repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="autolink">The Autolink object to be created for the repository</param>
        /// <remarks>See the <a href="https://docs.github.com/en/rest/repos/autolinks#create-an-autolink-reference-for-a-repository">API documentation</a> for more information.</remarks>
        Task<Autolink> Create(string owner, string repo, AutolinkRequest autolink);

        /// <summary>
        /// Deletes a single autolink reference by ID that was configured for the given repository
        /// </summary>
        /// <param name="owner">The account owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="autolinkId">The unique identifier of the autolink</param>
        /// <remarks>See the <a href="https://docs.github.com/en/rest/repos/autolinks#delete-an-autolink-reference-from-a-repository">API documentation</a> for more information.</remarks>
        Task Delete(string owner, string repo, int autolinkId);
    }
}
