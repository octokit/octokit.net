namespace Octokit.Reactive
{
    public class ObservableReactionsClient : IObservableReactionsClient
    {
        public ObservableReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
        }
    }
}
