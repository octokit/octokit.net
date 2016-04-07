using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Request body for starting a migration.
    /// </summary>
    /// <remarks>
    /// See <a href="https://developer.github.com/v3/migration/migrations/#start-a-migration">docs</a>
    /// for more information.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StartMigrationRequest
    {
        /// <summary>
        /// Parameter-less constructor needed for SimpleJsonSerializer.
        /// </summary>
        public StartMigrationRequest()
        { }

        public StartMigrationRequest(
            IReadOnlyList<string> repositories
            ) :
            this(repositories, false, false)
        { }

        /// <summary>
        /// Instantiate a new Migration Request object.
        /// </summary>
        /// <param name="repositories">List of repositories in {owner}/{repo} format.</param>
        /// <param name="lockRepositories">To lock the repos or not.</param>
        /// <param name="excludeAttachments">To exclude the attachments or not.</param>
        public StartMigrationRequest(
            IReadOnlyList<string> repositories,
            bool lockRepositories,
            bool excludeAttachments)
        {
            Repositories = repositories;
            LockRepositories = lockRepositories;
            ExcludeAttachments = excludeAttachments;
        }

        /// <summary>
        /// Required. A list of arrays indicating which repositories should be migrated.
        /// </summary>
        public IReadOnlyList<string> Repositories { get; private set; }

        /// <summary>
        /// Indicates whether repositories should be locked (to prevent manipulation) 
        /// while migrating data. Default: false.
        /// </summary>
        public bool LockRepositories { get; private set; }

        /// <summary>
        /// Indicates whether attachments should be excluded from the migration 
        /// (to reduce migration archive file size). Default: false.
        /// </summary>
        public bool ExcludeAttachments { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Repos: {0}", Repositories);
            }
        }
    }
}