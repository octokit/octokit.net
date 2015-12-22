namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/">Activity API documentation</a> for more information.
    /// </remarks>
    public class ActivitiesClient : ApiClient, IActivitiesClient
    {
        /// <summary>
        /// Instantiate a new GitHub Activities API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActivitiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Events = new EventsClient(apiConnection);
            Starring = new StarredClient(apiConnection);
            Watching = new WatchedClient(apiConnection);
            Feeds = new FeedsClient(apiConnection);
            Notifications = new NotificationsClient(apiConnection);
        }

        /// <summary>
        /// Client for the Events API
        /// </summary>
        public IEventsClient Events { get; private set; }
        /// <summary>
        /// Client for the Starring API
        /// </summary>
        public IStarredClient Starring { get; private set; }
        /// <summary>
        /// Client for the Watching API
        /// </summary>
        public IWatchedClient Watching { get; private set; }
        /// <summary>
        /// Client for the Feeds API
        /// </summary>
        public IFeedsClient Feeds { get; private set; }
        /// <summary>
        /// Client for the Notifications API
        /// </summary>
        public INotificationsClient Notifications { get; private set; }
    }
}
