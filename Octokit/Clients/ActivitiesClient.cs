namespace Octokit
{
    public class ActivitiesClient : ApiClient, IActivitiesClient
    {
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
