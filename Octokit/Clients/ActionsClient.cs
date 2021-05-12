namespace Octokit
{
    public class ActionsClient : IActionsClient
    {
        public ActionsClient(ApiConnection apiConnection)
        {
            Run = new WorkflowRunsClient(apiConnection);
        }

        public IWorkflowRunsClient Run { get; }
    }
}
