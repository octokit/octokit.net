namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflow jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflow jobs API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsWorkflowJobsClient : IObservableActionsWorkflowJobsClient
    {
        readonly IActionsWorkflowJobsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows jobs API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowJobsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows.Jobs;
        }
    }
}
