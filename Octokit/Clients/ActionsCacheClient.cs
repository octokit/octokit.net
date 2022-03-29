namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Cache API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/cache/">Actions Cache API documentation</a> for more information.
    /// </remarks>
    public class ActionsCacheClient : ApiClient, IActionsCacheClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Cache API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsCacheClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
