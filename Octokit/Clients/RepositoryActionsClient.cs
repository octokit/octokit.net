namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://docs.github.com/v3/actions">Repository Actions API documentation</a> for more details.
    /// </remarks>
    public class RepositoryActionsClient : ApiClient, IRepositoryActionsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Actions API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryActionsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Secrets = new RepositorySecretsClient(apiConnection);
            Variables = new RepositoryVariablesClient(apiConnection);
        }

        /// <summary>
        /// Client for GitHub's Repository Actions API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#secrets">Deployments API documentation</a> for more details
        /// </remarks>
        public IRepositorySecretsClient Secrets { get; set; }

        /// <summary>
        /// Client for GitHub's Repository Actions API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/free-pro-team@latest/rest/actions/variables?apiVersion=2022-11-28">Deployments API documentation</a> for more details
        /// </remarks>
        public IRepositoryVariablesClient Variables { get; set; }
    }
}
