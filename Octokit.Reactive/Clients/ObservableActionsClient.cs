namespace Octokit.Reactive
{
    public class ObservableActionsClient : IObservableActionsClient
    {
        public ObservableActionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Artifact = new ObservableWorkflowArtifactsClient(client);
            Secret = new ObservableSecretsClient(client);
            Runner = new ObservableRunnersClient(client);
            Workflow = new ObservableWorkflowsClient(client);
            Run = new ObservableWorkflowRunsClient(client);
            Job = new ObservableWorkflowJobsClient(client);
        }

        public IObservableWorkflowArtifactsClient Artifact { get; private set; }

        public IObservableSecretsClient Secret { get; private set; }

        public IObservableRunnersClient Runner { get; private set; }

        public IObservableWorkflowsClient Workflow { get; private set; }

        public IObservableWorkflowRunsClient Run { get; private set; }

        public IObservableWorkflowJobsClient Job { get; private set; }
    }
}
