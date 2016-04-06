namespace Octokit.Reactive
{
    public interface IObservableMigrationClient
    {
        /// <summary>
        /// A client for GitHub's Migrations API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/migration/migrations/">Enterprise License API documentation</a> for more information.
        /// </remarks>
        IObservableMigrationsClient Migrations { get; }
    }
}