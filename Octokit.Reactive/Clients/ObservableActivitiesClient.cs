namespace Octokit.Reactive
{
    public class ObservableActivitiesClient : IObservableActivitiesClient
    {
        public ObservableActivitiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Events = new ObservableEventsClient(client);
        }
        public IObservableEventsClient Events { get; private set; }
    }
}