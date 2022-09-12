using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a migration.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/migration/migrations/#start-a-migration">docs</a>
    /// for more information.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Migration
    {
        /// <summary>
        /// Parameter-less constructore needed for SimpleJsonSerializer.
        /// </summary>
        public Migration()
        { }

        public Migration(
            int id,
            string nodeId,
            string guid,
            MigrationState state,
            bool lockRepositories,
            bool excludeAttachments,
            string url,
            string createdAt,
            string updatedAt,
            IReadOnlyList<Repository> repositories)
        {
            Id = id;
            NodeId = nodeId;
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
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// Guid of migration.
        /// </summary>
        public string Guid { get; private set; }

        /// <summary>
        /// The state of migration. Can be one of pending, exporting, exported and failed.
        /// </summary>
        public StringEnum<MigrationState> State { get; private set; }

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

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Guid: {0}", Guid);
            }
        }

        /// <summary>
        /// State of a migration.
        /// </summary>
        /// <remarks>
        /// See: https://developer.github.com/v3/migration/migrations/#get-the-status-of-a-migration
        /// </remarks>
        public enum MigrationState
        {
            /// <summary>
            /// The migration hasn't started yet.
            /// </summary>
            [Parameter(Value = "pending")]
            Pending,

            /// <summary>
            /// The migration is in progress.
            /// </summary>
            [Parameter(Value = "exporting")]
            Exporting,

            /// <summary>
            /// The migration finished successfully.
            /// </summary>
            [Parameter(Value = "exported")]
            Exported,

            /// <summary>
            /// The migration failed.
            /// </summary>
            [Parameter(Value = "failed")]
            Failed
        }
    }
}
