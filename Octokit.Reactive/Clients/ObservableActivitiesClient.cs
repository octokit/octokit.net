namespace Octokit.Reactive
{
    public class ObservableActivitiesClient : IObservableActivitiesClient
    {
        public ObservableActivitiesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Events = new ObservableEventsClient(client);
            Watching = new ObservableWatchedClient(client);
            Starring = new ObservableStarredClient(client);
            Feeds = new ObservableFeedsClient(client);
        }
        public IObservableEventsClient Events { get; private set; }

        public IObservableWatchedClient Watching { get; private set; }

        public IObservableStarredClient Starring { get; private set; }

        public IObservableFeedsClient Feeds { get; private set; }
    }
}