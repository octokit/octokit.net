using System;
using System.Collections.Generic;
#if NET_45
using System.Collections.ObjectModel;
#endif
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's miscellaneous APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/misc/">Miscellaneous API documentation</a> for more details.
    /// </remarks>
    public class MiscellaneousClient : IMiscellaneousClient
    {
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new GitHub miscellaneous API client.
        /// </summary>
        /// <param name="connection">An API connection</param>
        public MiscellaneousClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            _connection = connection;
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IReadOnlyDictionary{TKey,TValue}"/> of emoji and their URI.</returns>
        public async Task<IReadOnlyList<Emoji>> GetEmojis()
        {
            var endpoint = new Uri("emojis", UriKind.Relative);
            var response = await _connection.Get<Dictionary<string, string>>(endpoint, null, null)
                                            .ConfigureAwait(false);
            return new ReadOnlyCollection<Emoji>(
                response.BodyAsObject.Select(kvp => new Emoji(kvp.Key, new Uri(kvp.Value))).ToArray());
        }

        /// <summary>
        /// Gets the rendered Markdown for the specified plain-text Markdown document.
        /// </summary>
        /// <param name="markdown">A plain-text Markdown document</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The rendered Markdown.</returns>
        public async Task<string> RenderRawMarkdown(string markdown)
        {
            var endpoint = new Uri("markdown/raw", UriKind.Relative);
            var response = await _connection.Post<string>(endpoint, markdown, "text/html", "text/plain")
                                            .ConfigureAwait(false);
            return response.Body;
        }
    }
}
