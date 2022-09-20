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
    public class EmojisClient : ApiClient, IEmojisClient
    {
        /// <summary>
        ///     Initializes a new GitHub emojis API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public EmojisClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        [ManualRoute("GET", "/emojis")]
        public Task<IReadOnlyList<Emoji>> GetAllEmojis()
        {
            return ApiConnection.GetAll<Emoji>(ApiUrls.Emojis());
        }
    }
}
