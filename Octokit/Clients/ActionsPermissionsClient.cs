namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Permissions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/permissions/">Actions Permissions API documentation</a> for more information.
    /// </remarks>
    public class ActionsPermissionsClient : ApiClient, IActionsPermissionsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Permissions API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsPermissionsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
