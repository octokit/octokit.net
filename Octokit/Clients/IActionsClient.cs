namespace Octokit
{
    public interface IActionsClient
    {
        IWorkflowArtifactsClient Artifact { get; }

        ISecretsClient Secret { get; }

        IRunnersClient Runner { get; }

        IWorkflowsClient Workflow { get; }

        IWorkflowJobsClient Job { get; }

        IWorkflowRunsClient Run { get; }
    }
}
