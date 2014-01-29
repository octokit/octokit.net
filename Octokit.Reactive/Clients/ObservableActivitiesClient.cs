namespace Octokit.Reactive
{
    public class ObservableActivitiesClient : IObservableActivitiesClient
    {
        public ObservableActivitiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Events = new ObservableEventsClient(client);
            Starred = new ObservableStarredClient(client);
        }
        public IObservableEventsClient Events { get; private set; }
        public IObservableStarredClient Starred { get; private set; }
    }
}