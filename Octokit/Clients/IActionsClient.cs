namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/">Actions API documentation</a> for more information.
    /// </remarks>
    public interface IActionsClient
    {
        /// <summary>
        /// Client for the Artifacts API.
        /// </summary>
        IActionsArtifactsClient Artifacts { get; }

        /// <summary>
        /// Client for the Cache API.
        /// </summary>
        IActionsCacheClient Cache { get; }


        /// <summary>
        /// Client for the OIDC API.
        /// </summary>
        IActionsOidcClient Oidc { get; }

        /// <summary>
        /// Client for the Permissions API.
        /// </summary>
        IActionsPermissionsClient Permissions { get; }

        /// <summary>
        /// Client for the Self-hosted runner groups API.
        /// </summary>
        IActionsSelfHostedRunnerGroupsClient SelfHostedRunnerGroups { get; }

        /// <summary>
        /// Client for the Self-hosted runners API.
        /// </summary>
        IActionsSelfHostedRunnersClient SelfHostedRunners { get; }

        /// <summary>
        /// Client for the Workflows API.
        /// </summary>
        IActionsWorkflowsClient Workflows { get; }
    }
}
