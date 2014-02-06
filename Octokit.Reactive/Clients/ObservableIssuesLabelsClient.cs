namespace Octokit.Reactive
{
    public class ObservableIssuesLabelsClient : IObservableIssuesLabelsClient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        IIssuesLabelsClient _client;

        public ObservableIssuesLabelsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Issue.Labels;
        }
    }
}