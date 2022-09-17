using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Emojis APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/emojis">Emojis API documentation</a> for more details.
    /// </remarks>
    public interface IObservableEmojisClient
    {
        /// <summary>
        /// Gets all the emojis available to use on GitHub.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="IObservable{Emoji}"/> of emoji and their URI.</returns>
        IObservable<Emoji> GetAllEmojis();
    }
}
