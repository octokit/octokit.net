using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Methods for the PubSubHubbub API.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/repos/hooks/#pubsubhubbub">PubSubHubbub API</a>
    /// </remarks>
    public interface IPubSubHubbubClient
    {
        /// <summary>
        /// Subscribe to a pubsub topic
        /// </summary>
        /// <param name="topic">A recoginized and supported pubsub topic.</param>
        /// <param name="callback">A callback url to be posted to when the topic event is fired.</param>
        /// <param name="secret">An optional shared secret used to generate a SHA1 HMAC of the outgoing body content.</param>
        /// <returns>Task.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        Task Subscribe(string topic, string callback, string secret = null);

        /// <summary>
        /// Unsubscribe from a pubsub topic
        /// </summary>
        /// <param name="topic">A recoginized pubsub topic.</param>
        /// <param name="callback">A callback url to be unsubscribed from.</param>
        /// <returns>Task.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        Task Unsubscribe(string topic, string callback);
    }
}