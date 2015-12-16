using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Repository
    {
        public Repository() { }

        public Repository(int id)
        {
            Id = id;
        }

        public Repository(string url, string htmlUrl, string cloneUrl, string gitUrl, string sshUrl, string svnUrl, string mirrorUrl, int id, User owner, string name, string fullName, string description, string homepage, string language, bool @private, bool fork, int forksCount, int stargazersCount, int subscribersCount, string defaultBranch, int openIssuesCount, DateTimeOffset? pushedAt, DateTimeOffset createdAt, DateTimeOffset updatedAt, RepositoryPermissions permissions, User organization, Repository parent, Repository source, bool hasIssues, bool hasWiki, bool hasDownloads)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            CloneUrl = cloneUrl;
            GitUrl = gitUrl;
            SshUrl = sshUrl;
            SvnUrl = svnUrl;
            MirrorUrl = mirrorUrl;
            Id = id;
            Owner = owner;
            Name = name;
            FullName = fullName;
            Description = description;
            Homepage = homepage;
            Language = language;
            Private = @private;
            Fork = fork;
            ForksCount = forksCount;
            StargazersCount = stargazersCount;
#pragma warning disable 612,618
            SubscribersCount = subscribersCount;
#pragma warning restore 612,618
            DefaultBranch = defaultBranch;
            OpenIssuesCount = openIssuesCount;
            PushedAt = pushedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Permissions = permissions;
#pragma warning disable 612, 618
            Organization = organization;
#pragma warning restore 612, 618
            Parent = parent;
            Source = source;
            HasIssues = hasIssues;
            HasWiki = hasWiki;
            HasDownloads = hasDownloads;
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

        [Obsolete("This property has been obsoleted. Please use WatchedClient.GetAllWatchers instead.")]
        public int SubscribersCount { get; protected set; }

        public string DefaultBranch { get; protected set; }

        public int OpenIssuesCount { get; protected set; }

        public DateTimeOffset? PushedAt { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public RepositoryPermissions Permissions { get; protected set; }

        [Obsolete("This property has been obsoleted by Repository.Owner. Please use Repository.Owner.Type instead.")]
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
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository: Id: {0} Owner: {1}, Name: {2}", Id, Owner, Name);
            }
        }
    }
}
