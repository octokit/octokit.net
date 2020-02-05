namespace Octokit
{
    public class ActionsClient : IActionsClient
    {
        public ActionsClient(ApiConnection apiConnection)
        {
            Artifact = new WorkflowArtifactsClient(apiConnection);
            Secret = new SecretsClient(apiConnection);
            Runner = new RunnersClient(apiConnection);
            Workflow = new WorkflowsClient(apiConnection);
            Run = new WorkflowRunsClient(apiConnection);
            Job = new WorkflowJobsClient(apiConnection);
        }

        public IWorkflowArtifactsClient Artifact { get; private set; }

        public ISecretsClient Secret { get; private set; }

        public IRunnersClient Runner { get; private set; }

        public IWorkflowsClient Workflow { get; private set; }

        public IWorkflowRunsClient Run { get; private set; }

        public IWorkflowJobsClient Job { get; private set; }

    }
}
