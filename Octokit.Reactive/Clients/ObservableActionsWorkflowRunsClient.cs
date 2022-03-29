namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflow runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflow runs API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsWorkflowRunsClient : IObservableActionsWorkflowRunsClient
    {
        readonly IActionsWorkflowRunsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows runs API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows.Runs;
        }
    }
}
