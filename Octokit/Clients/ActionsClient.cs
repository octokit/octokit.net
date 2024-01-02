using Octokit.Clients;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/">Actions API documentation</a> for more information.
    /// </remarks>
    public class ActionsClient : ApiClient, IActionsClient
    {
        /// <summary>
        /// Instantiate a new GitHub Actions API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public ActionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Artifacts = new ActionsArtifactsClient(apiConnection);
            Cache = new ActionsCacheClient(apiConnection);
            Oidc = new ActionsOidcClient(apiConnection);
            Permissions = new ActionsPermissionsClient(apiConnection);
            SelfHostedRunnerGroups = new ActionsSelfHostedRunnerGroupsClient(apiConnection);
            SelfHostedRunners = new ActionsSelfHostedRunnersClient(apiConnection);
            Workflows = new ActionsWorkflowsClient(apiConnection);
        }

        /// <summary>
        /// Client for the Artifacts API.
        /// </summary>
        public IActionsArtifactsClient Artifacts { get; private set; }

        /// <summary>
        /// Client for the Cache API.
        /// </summary>
        public IActionsCacheClient Cache { get; private set; }

        /// <summary>
        /// Client for the OIDC API.
        /// </summary>
        public IActionsOidcClient Oidc { get; private set; }

        /// <summary>
        /// Client for the Permissions API.
        /// </summary>
        public IActionsPermissionsClient Permissions { get; private set; }

        /// <summary>
        /// Client for the Self-hosted runner groups API.
        /// </summary>
        public IActionsSelfHostedRunnerGroupsClient SelfHostedRunnerGroups { get; private set; }

        /// <summary>
        /// Client for the Self-hosted runners API.
        /// </summary>
        public IActionsSelfHostedRunnersClient SelfHostedRunners { get; private set; }

        /// <summary>
        /// Client for the Workflows API.
        /// </summary>
        public IActionsWorkflowsClient Workflows { get; private set; }
    }
}
