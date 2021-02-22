using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Repository
    {
        public Repository() { }

        public Repository(long id)
        {
            Id = id;
        }

        public Repository(string url, string htmlUrl, string cloneUrl, string gitUrl, string sshUrl, string svnUrl, string mirrorUrl, long id, string nodeId, User owner, string name, string fullName, bool isTemplate, string description, string homepage, string language, bool @private, bool fork, int forksCount, int stargazersCount, string defaultBranch, int openIssuesCount, DateTimeOffset? pushedAt, DateTimeOffset createdAt, DateTimeOffset updatedAt, RepositoryPermissions permissions, Repository parent, Repository source, LicenseMetadata license, bool hasIssues, bool hasWiki, bool hasDownloads, bool hasPages, int subscribersCount, long size, bool? allowRebaseMerge, bool? allowSquashMerge, bool? allowMergeCommit, bool archived, int watchersCount, bool? deleteBranchOnMerge, RepositoryVisibility visibility, IEnumerable<string> topics)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            CloneUrl = cloneUrl;
            GitUrl = gitUrl;
            SshUrl = sshUrl;
            SvnUrl = svnUrl;
            MirrorUrl = mirrorUrl;
            Id = id;
            NodeId = nodeId;
            Owner = owner;
            Name = name;
            FullName = fullName;
            IsTemplate = isTemplate;
            Description = description;
            Homepage = homepage;
            Language = language;
            Private = @private;
            Fork = fork;
            ForksCount = forksCount;
            StargazersCount = stargazersCount;
            DefaultBranch = defaultBranch;
            OpenIssuesCount = openIssuesCount;
            PushedAt = pushedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Permissions = permissions;
            Parent = parent;
            Source = source;
            License = license;
            HasIssues = hasIssues;
            HasWiki = hasWiki;
            HasDownloads = hasDownloads;
            HasPages = hasPages;
            SubscribersCount = subscribersCount;
            Size = size;
            AllowRebaseMerge = allowRebaseMerge;
            AllowSquashMerge = allowSquashMerge;
            AllowMergeCommit = allowMergeCommit;
            Archived = archived;
            WatchersCount = watchersCount;
            Topics = topics.ToList();
            DeleteBranchOnMerge = deleteBranchOnMerge;
            Visibility = visibility;
        }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string CloneUrl { get; protected set; }

        public string GitUrl { get; protected set; }

        public string SshUrl { get; protected set; }

        public string SvnUrl { get; protected set; }

        public string MirrorUrl { get; protected set; }

        public long Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        public User Owner { get; protected set; }

        public string Name { get; protected set; }

        public string FullName { get; protected set; }

        public bool IsTemplate { get; protected set; }

        public string Description { get; protected set; }

        public string Homepage { get; protected set; }

        public string Language { get; protected set; }

        public bool Private { get; protected set; }

        public bool Fork { get; protected set; }

        public int ForksCount { get; protected set; }

        public int StargazersCount { get; protected set; }

        public int WatchersCount { get; protected set; }

        public string DefaultBranch { get; protected set; }

        public int OpenIssuesCount { get; protected set; }

        public DateTimeOffset? PushedAt { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        public RepositoryPermissions Permissions { get; protected set; }

        public Repository Parent { get; protected set; }

        public Repository Source { get; protected set; }

        public LicenseMetadata License { get; protected set; }

        public bool HasIssues { get; protected set; }

        public bool HasWiki { get; protected set; }

        public bool HasDownloads { get; protected set; }

        public bool? AllowRebaseMerge { get; protected set; }

        public bool? AllowSquashMerge { get; protected set; }

        public bool? AllowMergeCommit { get; protected set; }

        public bool HasPages { get; protected set; }

        [Obsolete("Update your code to use WatchersCount as this field will stop containing data in the future")]
        public int SubscribersCount { get; protected set; }

        public long Size { get; protected set; }

        public bool Archived { get; protected set; }

        public IReadOnlyList<string> Topics { get; protected set; }

        public bool? DeleteBranchOnMerge { get; protected set; }

        public RepositoryVisibility? Visibility { get; protected set; }

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
