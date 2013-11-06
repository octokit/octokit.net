namespace Octokit
{
    public class ActivitiesClient : ApiClient, IActivitiesClient
    {
        public ActivitiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Events = new EventsClient(apiConnection);
        }

        public IEventsClient Events { get; private set; }
    }
}
