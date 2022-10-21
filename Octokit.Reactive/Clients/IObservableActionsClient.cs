namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/">Actions API documentation</a> for more information.
    /// </remarks>
    public interface IObservableActionsClient
    {
        /// <summary>
        /// Client for the Artifacts API.
        /// </summary>
        IObservableActionsArtifactsClient Artifacts { get; }

        /// <summary>
        /// Client for the Cache API.
        /// </summary>
        IObservableActionsCacheClient Cache { get; }

        /// <summary>
        /// Client for the Permissions API.
        /// </summary>
        IObservableActionsPermissionsClient Permissions { get; }

        /// <summary>
        /// Client for the Self-hosted runner groups API.
        /// </summary>
        IObservableActionsSelfHostedRunnerGroupsClient SelfHostedRunnerGroups { get; }

        /// <summary>
        /// Client for the Self-hosted runners API.
        /// </summary>
        IObservableActionsSelfHostedRunnersClient SelfHostedRunners { get; }

        /// <summary>
        /// Client for the Workflows API.
        /// </summary>
        IObservableActionsWorkflowsClient Workflows { get; }
    }
}
