namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Migration API. These APIs help you move projects to or from GitHub.
    /// </summary>
    /// <remarks>
    /// Docs: https://developer.github.com/v3/migration/
    /// </remarks>
    public interface IMigrationClient
    {
        /// <summary>
        /// The Enterprise Migrations API lets you move a repository from GitHub to GitHub Enterprise.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/#enterprise-migrations
        /// </remarks>
        IMigrationsClient Migrations { get; }
    }
}