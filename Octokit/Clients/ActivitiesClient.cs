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
        }

        public IEventsClient Events { get; private set; }
        public IStarredClient Starring { get; private set; }
        public IWatchedClient Watching { get; private set; }
        public IFeedsClient Feeds { get; private set; }
    }
}
