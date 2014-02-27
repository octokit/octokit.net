using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Feeds API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/feeds/">Feeds API documentation</a> for more information
    /// </remarks>
    public interface IObservableFeedsClient
    {
        /// <summary>
        /// Gets all the feeds available to the authenticating user
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/feeds/#list-feeds
        /// </remarks>
        /// <returns>All the public <see cref="Feed"/>s for the particular user.</returns>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Feed> GetFeeds();
    }
}