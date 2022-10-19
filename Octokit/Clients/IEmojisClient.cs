using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Emojis APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/emojis">Emojis API documentation</a> for more details.
    /// </remarks>
    public interface IEmojisClient
    {
        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<Emoji>> GetAllEmojis();
    }
}