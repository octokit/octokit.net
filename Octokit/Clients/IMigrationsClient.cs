using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// An interface for GitHub's Migrations API client.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/migration/migrations/">docs</a>
    /// for more information.
    /// </remarks>
    public interface IMigrationsClient
    {
        /// <summary>
        /// Starts a new migration specified for the given organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#start-a-migration
        /// </remarks>
        /// <param name="org">The organization for which to start a migration.</param>
        /// <param name="migration">Specifies parameters for the migration in a 
        /// <see cref="StartMigrationRequest"/> object.</param>
        /// <returns>The started migration.</returns>
        Task<Migration> Start(
            string org,
            StartMigrationRequest migration);

        /// <summary>
        /// Gets the list of the most recent migrations of the organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-a-list-of-migrations
        /// </remarks>
        /// <param name="org">The organization of which to list migrations.</param>
        /// <returns>List of most recent <see cref="Migration"/>s.</returns>
        Task<IReadOnlyList<Migration>> GetAll(
            string org);

        /// <summary>
        /// Gets the list of the most recent migrations of the organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-a-list-of-migrations
        /// </remarks>
        /// <param name="org">The organization of which to list migrations.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>List of most recent <see cref="Migration"/>s.</returns>
        Task<IReadOnlyList<Migration>> GetAll(
            string org,
            ApiOptions options);

        /// <summary>
        /// Get the status of a migration
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-the-status-of-a-migration
        /// </remarks>
        /// <param name="org">The organization which is migrating.</param>
        /// <param name="id">Migration Id of the organization.</param>
        /// <returns>A <see cref="Migration"/> object representing the state of migration.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Migration> Get(
            string org,
            int id);

        /// <summary>
        /// Get the migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#download-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The Id of the migration.</param>
        /// <returns>The binary contents of the archive as a byte array.</returns>
        Task<byte[]> GetArchive(
            string org,
            int id);

        /// <summary>
        /// Deletes a previous migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#delete-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The Id of the migration.</param>
        /// <returns></returns>
        Task DeleteArchive(
            string org,
            int id);

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
        Task UnlockRepository(
            string org,
            int id,
            string repo);
    }
}
