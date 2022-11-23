namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Artifacts API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/artifacts/">Actions Artifacts API documentation</a> for more information.
    /// </remarks>
    public class ActionsArtifactsClient : ApiClient, IActionsArtifactsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Artifacts API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsArtifactsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
