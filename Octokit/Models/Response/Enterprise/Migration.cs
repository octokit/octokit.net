using System.Collections.Generic;

namespace Octokit
{
    public class Migration
    {
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

        public int Id { get; private set; }

        public string Guid { get; private set; }

        public string State { get; private set; }

        public bool LockRepositories { get; private set; }

        public bool ExcludeAttachments { get; private set; }

        public string Url { get; private set; }

        public string CreatedAt { get; private set; }

        public string UpdatedAt { get; private set; }

        public IReadOnlyList<Repository> Repositories { get; private set; }


    }
}