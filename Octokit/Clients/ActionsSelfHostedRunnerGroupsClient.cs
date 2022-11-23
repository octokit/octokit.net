namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Self-hosted runner groups API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runner-groups/">Actions Self-hosted runner groups API documentation</a> for more information.
    /// </remarks>
    public class ActionsSelfHostedRunnerGroupsClient : ApiClient, IActionsSelfHostedRunnerGroupsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Self-hosted runner groups API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsSelfHostedRunnerGroupsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
