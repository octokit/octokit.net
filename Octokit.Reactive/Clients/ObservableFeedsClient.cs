using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Feeds API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/feeds/">Feeds API documentation</a> for more information
    /// </remarks>
    public class ObservableFeedsClient : IObservableFeedsClient
    {
        readonly IFeedsClient _client;

        public ObservableFeedsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Activity.Feeds;
        }

        /// <summary>
        /// Gets all the feeds available to the authenticating user
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/feeds/#list-feeds
        /// </remarks>
        /// <returns>All the public <see cref="Feed"/>s for the particular user.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IObservable<Feed> GetFeeds()
        {
            return _client.GetFeeds().ToObservable();
        }
    }
}