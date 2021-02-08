namespace Octokit
{
    public interface IActionsClient
    {
        IWorkflowRunsClient Run { get; }
    }
}
