using System;
using System.Threading;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// An interface for GitHub's Migrations API client.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/migration/migrations/">docs</a>
    /// for more information.
    /// </remarks>
    public class ObservableMigrationsClient : IObservableMigrationsClient
    {
        private readonly IMigrationsClient _client;
        private readonly IConnection _connection;

        /// <summary>
        /// Instantiates a GitHub Migrations API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient"/> for making requests.</param>
        public ObservableMigrationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Migration.Migrations;
            _connection = client.Connection;
        }

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
        public IObservable<Migration> Start(string org, StartMigrationRequest migration, CancellationToken cancellationToken = default)
        {
            return _client.Start(org, migration, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the list of the most recent migrations of the the organization.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-a-list-of-migrations
        /// </remarks>
        /// <param name="org">The organization of which to list migrations.</param>
        /// <returns>List of most recent <see cref="Migration"/>s.</returns>
        public IObservable<Migration> GetAll(string org, CancellationToken cancellationToken = default)
        {
            return GetAll(org, ApiOptions.None, cancellationToken);
        }

        public IObservable<Migration> GetAll(string org, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Migration>(ApiUrls.EnterpriseMigrations(org), null, options, cancellationToken);
        }

        /// <summary>
        /// Get the status of a migration
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/migration/migrations/#get-the-status-of-a-migration
        /// </remarks>
        /// <param name="org">The organization which is migrating.</param>
        /// <param name="id">Migrations Id of the organization.</param>
        /// <returns>A <see cref="Migration"/> object representing the state of migration.</returns>
        public IObservable<Migration> Get(string org, long id, CancellationToken cancellationToken = default)
        {
            return _client.Get(org, id, cancellationToken).ToObservable();
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
        public IObservable<byte[]> GetArchive(string org, long id, CancellationToken cancellationToken = default)
        {
            return _client.GetArchive(org, id, cancellationToken).ToObservable();
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
        public IObservable<Unit> DeleteArchive(string org, long id, CancellationToken cancellationToken = default)
        {
            return _client.DeleteArchive(org, id, cancellationToken).ToObservable();
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
        public IObservable<Unit> UnlockRepository(string org, long id, string repo, CancellationToken cancellationToken = default)
        {
            return _client.UnlockRepository(org, id, repo, cancellationToken).ToObservable();
        }
    }
}
