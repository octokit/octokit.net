using System.Collections.Generic;

namespace Octokit
{
    public class StartMigrationRequest
    {
        public StartMigrationRequest(
            IReadOnlyList<string> repositories
            ) :
            this(repositories, false, false)
        { }

        public StartMigrationRequest(
            IReadOnlyList<string> repositories, 
            bool lockRepositories) :
            this(repositories, lockRepositories, false)
        { }

        public StartMigrationRequest(
            IReadOnlyList<string> repositories,
            bool lockRepositories,
            bool excludeAttachments)
        {
            this.Repositories = repositories;
            this.LockRepositories = lockRepositories;
            this.ExcludeAttachments = excludeAttachments;
        }

        public IReadOnlyList<string> Repositories { get; private set; }

        public bool LockRepositories { get; private set; }

        public bool ExcludeAttachments { get; private set; }
    }
}