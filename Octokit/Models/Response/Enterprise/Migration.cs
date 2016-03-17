using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Represents a migration.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/migration/migrations/#start-a-migration">docs</a>
    /// for more information.
    /// </remarks>
    public class Migration
    {
        /// <summary>
        /// Parameter-less constructore needed for SimpleJsonSerializer.
        /// </summary>
        public Migration()
        { }

        public Migration(
            int id,
            string guid,
            string state,
            bool lockRepositories,
            bool excludeAttachments,
            string url,
            string createdAt,
            string updatedAt,
            IReadOnlyList<Repository> repositories)
        {
            Id = id;
            Guid = guid;
            State = state;
            LockRepositories = lockRepositories;
            ExcludeAttachments = excludeAttachments;
            Url = url;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Repositories = repositories;
        }

        /// <summary>
        /// Id of migration.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Guid of migration.
        /// </summary>
        public string Guid { get; private set; }

        /// <summary>
        /// The state of migration. Can be one of pending, exporting, exported and failed.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Whether to lock repositories.
        /// </summary>
        public bool LockRepositories { get; private set; }

        /// <summary>
        /// Whether attachments are excluded or not.
        /// </summary>
        public bool ExcludeAttachments { get; private set; }

        /// <summary>
        /// URL of migration.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Time of migration creation.
        /// </summary>
        public string CreatedAt { get; private set; }

        /// <summary>
        /// Time of migration updation.
        /// </summary>
        public string UpdatedAt { get; private set; }

        /// <summary>
        /// List of locked repositories.
        /// </summary>
        public IReadOnlyList<Repository> Repositories { get; private set; }


    }
}