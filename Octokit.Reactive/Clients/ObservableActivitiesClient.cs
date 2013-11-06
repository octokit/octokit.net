namespace Octokit.Reactive
{
    public class ObservableActivitiesClient : IObservableActivitiesClient
    {
        public ObservableActivitiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Event = new ObservableEventsClient(client);
        }
        public IObservableEventsClient Event { get; private set; }
    }
}