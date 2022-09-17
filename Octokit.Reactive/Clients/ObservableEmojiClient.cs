using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Emojis APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/emojis">Emojis API documentation</a> for more details.
    /// </remarks>
    public class ObservableEmojisClient : IObservableEmojisClient
    {
        private readonly IEmojisClient _client;

        public ObservableEmojisClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Emojis;
        }

        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IObservable{Emoji}"/> of emoji and their URI.</returns>
        public IObservable<Emoji> GetAllEmojis()
        {
            return _client.GetAllEmojis().ToObservable().SelectMany(e => e);
        }
    }
}
