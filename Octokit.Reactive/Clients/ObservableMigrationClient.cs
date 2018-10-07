namespace Octokit.Reactive
{
    public class ObservableMigrationClient : IObservableMigrationClient
    {
        public ObservableMigrationClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Migrations = new ObservableMigrationsClient(client);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Migrations API.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/#enterprise-migrations
        /// </remarks>
        public IObservableMigrationsClient Migrations { get; private set; }
    }
}