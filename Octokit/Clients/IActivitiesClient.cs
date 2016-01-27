namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/">Activity API documentation</a> for more information.
    /// </remarks>
    public interface IActivitiesClient
    {
        /// <summary>
        /// Client for the Events API
        /// </summary>
        IEventsClient Events { get; }

        /// <summary>
        /// Client for the Starring API
        /// </summary>
        IStarredClient Starring { get; }

        /// <summary>
        /// Client for the Watching API
        /// </summary>
        IWatchedClient Watching { get; }

        /// <summary>
        /// Client for the Feeds API
        /// </summary>
        IFeedsClient Feeds { get; }

        /// <summary>
        /// Client for the Notifications API
        /// </summary>
        INotificationsClient Notifications { get; }
    }
}