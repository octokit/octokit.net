namespace Octokit.Reactive
{
    public class ObservableActionsClient : IObservableActionsClient
    {
        public ObservableActionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Run = new ObservableWorkflowRunsClient(client);
        }

        public IObservableWorkflowRunsClient Run { get; private set; }
    }
}
