namespace Octokit
{
    public class ActivitiesClient : ApiClient, IActivitiesClient
    {
        /// <summary>
        /// Instatiate a new GitHub Activities API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActivitiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Events = new EventsClient(apiConnection);
            Starring = new StarredClient(apiConnection);
        }

        public IEventsClient Events { get; private set; }
        public IStarredClient Starring { get; private set; }
    }
}
