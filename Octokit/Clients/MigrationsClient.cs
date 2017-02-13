using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Migrations API.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/migration/migrations/">docs</a>
    /// for more information.
    /// </remarks>
    public class MigrationsClient : ApiClient, IMigrationsClient
    {
        /// <summary>
        /// Instantiates a GitHub Migrations API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public MigrationsClient(IApiConnection apiConnection) : base(apiConnection)
        { }

        /// <summary>
        /// Starts a new migration specified for the given organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#start-a-migration
        /// </remarks>
        /// <param name="org">The organization for which to start a migration.</param>
        /// <param name="migration">Sprcifies parameters for the migration in a 
        /// <see cref="StartMigrationRequest"/> object.</param>
        /// <returns>The started migration.</returns>
        public async Task<Migration> Start(string org, StartMigrationRequest migration)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(migration, "migration");

            var endpoint = ApiUrls.EnterpriseMigrations(org);

            return await ApiConnection.Post<Migration>(endpoint, migration, AcceptHeaders.MigrationsApiPreview).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of the most recent migrations of the the organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-a-list-of-migrations
        /// </remarks>
        /// <param name="org">The organization of which to list migrations.</param>
        /// <returns>List of most recent <see cref="Migration"/>s.</returns>
        public async Task<List<Migration>> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrations(org);

            return await ApiConnection.Get<List<Migration>>(endpoint, null, AcceptHeaders.MigrationsApiPreview).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the status of a migration
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-the-status-of-a-migration
        /// </remarks>
        /// <param name="org">The organization which is migrating.</param>
        /// <param name="id">Migration Id of the organization.</param>
        /// <returns>A <see cref="Migration"/> object representing the state of migration.</returns>
        public async Task<Migration> Get(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationById(org, id);

            return await ApiConnection.Get<Migration>(endpoint, null, AcceptHeaders.MigrationsApiPreview).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#download-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The Id of the migration.</param>
        /// <returns>The binary contents of the archive as a byte array.</returns>
        public async Task<byte[]> GetArchive(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationArchive(org, id);
            var response = await Connection.Get<byte[]>(endpoint, null, AcceptHeaders.MigrationsApiPreview).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Deletes a previous migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#delete-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The Id of the migration.</param>
        /// <returns></returns>
        public Task DeleteArchive(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationArchive(org, id);

            return ApiConnection.Delete(endpoint, new object(), AcceptHeaders.MigrationsApiPreview);
        }

        /// <summary>
        /// Unlocks a repository that was locked for migration.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#unlock-a-repository
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The Id of the migration.</param>
        /// <param name="repo">The repo to unlock.</param>
        /// <returns></returns>
        public Task UnlockRepository(string org, int id, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            var endpoint = ApiUrls.EnterpriseMigrationUnlockRepository(org, id, repo);

            return ApiConnection.Delete(endpoint, new object(), AcceptHeaders.MigrationsApiPreview);
        }
    }
}
