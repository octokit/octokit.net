using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Repository
    {
        public Repository()
        {
        }

        public Repository(int id)
        {
            Id = id;
        }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string CloneUrl { get; protected set; }

        public string GitUrl { get; protected set; }

        public string SshUrl { get; protected set; }

        public string SvnUrl { get; protected set; }

        public string MirrorUrl { get; protected set; }

        public int Id { get; protected set; }

        public User Owner { get; protected set; }

        public string Name { get; protected set; }

        public string FullName { get; protected set; }

        public string Description { get; protected set; }

        public string Homepage { get; protected set; }

        public string Language { get; protected set; }

        public bool Private { get; protected set; }

        public bool Fork { get; protected set; }

        public int ForksCount { get; protected set; }

        public int StargazersCount { get; protected set; }

        public int WatchersCount { get; protected set; }

        public int SubscribersCount { get; protected set; }

        public string DefaultBranch { get; protected set; }

        public int OpenIssuesCount { get; protected set; }

        public DateTimeOffset? PushedAt { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public RepositoryPermissions Permissions { get; protected set; }

        public User Organization { get; protected set; }

        public Repository Parent { get; protected set; }

        public Repository Source { get; protected set; }

        public bool HasIssues { get; protected set; }

        public bool HasWiki { get; protected set; }

        public bool HasDownloads { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Repository: Id: {0} Owner: {1}, Name: {2}", Id, Owner, Name);
            }
        }
    }
}
