namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/secrets/">Actions Secrets API documentation</a> for more information.
    /// </remarks>
    public class ActionsSecretsClient : ApiClient, IActionsSecretsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Secrets API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsSecretsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
