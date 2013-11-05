namespace Octokit
{
    public class ActivitiesClient : ApiClient, IActivitiesClient
    {
        public ActivitiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Event = new EventsClient(apiConnection);
        }

        public IEventsClient Event { get; private set; }
    }
}
