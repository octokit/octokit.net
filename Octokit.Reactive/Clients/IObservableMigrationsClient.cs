using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// An interface for GitHub's Migrations API client.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/migration/migrations/">docs</a>
    /// for more information.
    /// </remarks>
    public interface IObservableMigrationsClient
    {
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
        IObservable<Migration> Start(
            string org,
            StartMigrationRequest migration);

        /// <summary>
        /// Gets the list of the most recent migrations of the the organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-a-list-of-migrations
        /// </remarks>
        /// <param name="org">The organization of which to list migrations.</param>
        /// <returns>List of most recent <see cref="Migration"/>s.</returns>
        IObservable<List<Migration>> GetAll(
            string org);

        /// <summary>
        /// Get the status of a migration
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-the-status-of-a-migration
        /// </remarks>
        /// <param name="org">The organization which is migrating.</param>
        /// <param name="id">Migration ID of the organization.</param>
        /// <returns>A <see cref="Migration"/> object representing the state of migration.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Migration> Get(
           string org,
           int id);

        /// <summary>
        /// Fetches the URL to a migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#download-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The ID of the migration.</param>
        /// <returns>URL as a string of the download link of the archive.</returns>
        IObservable<string> GetArchive(
            string org,
            int id);

        /// <summary>
        /// Deletes a previous migration archive.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#delete-a-migration-archive
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The ID of the migration.</param>
        /// <returns></returns>
        IObservable<Unit> DeleteArchive(
            string org,
            int id);

        /// <summary>
        /// Unlocks a repository that was locked for migration.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#unlock-a-repository
        /// </remarks>
        /// <param name="org">The organization of which the migration was.</param>
        /// <param name="id">The ID of the migration.</param>
        /// <param name="repo">The repo to unlock.</param>
        /// <returns></returns>
        IObservable<Unit> UnlockRepository(
            string org,
            int id,
            string repo);
    }
}
