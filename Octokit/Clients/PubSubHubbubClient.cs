using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Methods for the PubSubHubbub API.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/repos/hooks/#pubsubhubbub">PubSubHubbub API</a>
    /// </remarks>
    public class PubSubHubbubClient : ApiClient, IPubSubHubbubClient
    {
        /// <summary>
        /// Initializes a new PubSubHubbub client.
        /// </summary>
        /// <param name="apiConnection">The client's connection</param>
        public PubSubHubbubClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Subscribe to a pubsub topic
        /// </summary>
        /// <param name="topic">A recoginized and supported pubsub topic.</param>
        /// <param name="callback">A callback url to be posted to when the topic event is fired.</param>
        /// <param name="secret">An optional shared secret used to generate a SHA1 HMAC of the outgoing body content.</param>
        /// <returns>Task.</returns>
        public Task Subscribe(string topic, string callback, string secret)
        {
            Ensure.ArgumentNotNullOrEmptyString(topic, "topic");
            Ensure.ArgumentNotNullOrEmptyString(callback, "callback");

            var options = new Dictionary<string, string>()
            {
                { "hub.callback", callback },
                { "hub.mode", "subscribe"},
                { "hub.topic", topic },
            };

            if (!string.IsNullOrWhiteSpace(secret))
            {
                options["hub.secret"] = secret;
            }

            return PubSubHubbubRequest(options);
        }

        /// <summary>
        /// Unsubscribe from a pubsub topic
        /// </summary>
        /// <param name="topic">A recoginized pubsub topic.</param>
        /// <param name="callback">A callback url to be unsubscribed from.</param>
        /// <returns>Task.</returns>
        public Task Unsubscribe(string topic, string callback)
        {
            Ensure.ArgumentNotNullOrEmptyString(topic, "topic");
            Ensure.ArgumentNotNullOrEmptyString(callback, "callback");

            var options = new Dictionary<string, string>()
            {
                { "hub.callback", callback },
                { "hub.mode", "unsubscribe"},
                { "hub.topic", topic },
            };

            return PubSubHubbubRequest(options);
        }

        private async Task PubSubHubbubRequest(IDictionary<string, string> options)
        {
            var response = await Connection.Post<object>(new Uri("hub", UriKind.Relative), new FormUrlEncodedContent(options), null, null).ConfigureAwait(false);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 204", response.StatusCode);
            }
        }
    }
}