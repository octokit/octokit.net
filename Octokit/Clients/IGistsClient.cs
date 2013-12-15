using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Gists API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/gists/">Gists API documentation</a> for more information.
    /// </remarks>
    public interface IGistsClient
    {
        IGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<Gist> Get(string id);

        /// <summary>
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <returns>A list with the gists</returns>
        Task<IReadOnlyList<Gist>> GetAllForUser(string user);

    }
}