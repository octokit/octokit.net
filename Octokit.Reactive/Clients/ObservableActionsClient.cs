namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/">Actions API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsClient : IObservableActionsClient
    {
        /// <summary>
        /// Instantiate a new GitHub Actions API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Artifacts = new ObservableActionsArtifactsClient(client);
            Cache = new ObservableActionsCacheClient(client);
            Permissions = new ObservableActionsPermissionsClient(client);
            SelfHostedRunnerGroups = new ObservableActionsSelfHostedRunnerGroupsClient(client);
            SelfHostedRunners = new ObservableActionsSelfHostedRunnersClient(client);
            Workflows = new ObservableActionsWorkflowsClient(client);
        }

        /// <summary>
        /// Client for the Artifacts API.
        /// </summary>
        public IObservableActionsArtifactsClient Artifacts { get; private set; }

        /// <summary>
        /// Client for the Cache API.
        /// </summary>
        public IObservableActionsCacheClient Cache { get; private set; }

        /// <summary>
        /// Client for the Permissions API.
        /// </summary>
        public IObservableActionsPermissionsClient Permissions { get; private set; }

        /// <summary>
        /// Client for the Self-hosted runner groups API.
        /// </summary>
        public IObservableActionsSelfHostedRunnerGroupsClient SelfHostedRunnerGroups { get; private set; }

        /// <summary>
        /// Client for the Self-hosted runners API.
        /// </summary>
        public IObservableActionsSelfHostedRunnersClient SelfHostedRunners { get; private set; }

        /// <summary>
        /// Client for the Workflows API.
        /// </summary>
        public IObservableActionsWorkflowsClient Workflows { get; private set; }
    }
}
