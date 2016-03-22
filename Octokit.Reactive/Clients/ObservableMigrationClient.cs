namespace Octokit.Reactive
{
    public class ObservableMigrationClient : IObservableMigrationClient
    {
        public ObservableMigrationClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Migration = new ObservableMigrationsClient(client);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Migrations API.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/#enterprise-migrations
        /// </remarks>
        public IObservableMigrationsClient Migration { get; private set; }
    }
}