namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Migration API. These APIs help you move projects to or from GitHub.
    /// </summary>
    /// <remarks>
    /// Docs: https://developer.github.com/v3/migration/
    /// </remarks>
    public class MigrationClient : ApiClient, IMigrationClient
    {
        /// <summary>
        /// Instantiate a new GitHub Migration API client and its sub-APIs.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public MigrationClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Migrations = new MigrationsClient(apiConnection);
        }

        /// <summary>
        /// The Enterprise Migrations API lets you move a repository from GitHub to GitHub Enterprise.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/#enterprise-migrations
        /// </remarks>
        public IMigrationsClient Migrations { get; private set; }
    }
}