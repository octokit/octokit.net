using System;
using System.Collections.Generic;
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

        public Repository(string url, string htmlUrl, string cloneUrl, string gitUrl, string sshUrl, string svnUrl, string mirrorUrl, string archiveUrl, long id, string nodeId, User owner, string name, string fullName, bool isTemplate, string description, string homepage, string language, bool @private, bool fork, int forksCount, int stargazersCount, string defaultBranch, int openIssuesCount, DateTimeOffset? pushedAt, DateTimeOffset createdAt, DateTimeOffset updatedAt, RepositoryPermissions permissions, Repository parent, Repository source, LicenseMetadata license, bool hasDiscussions, bool hasIssues, bool hasWiki, bool hasDownloads, bool hasPages, int subscribersCount, long size, bool? allowRebaseMerge, bool? allowSquashMerge, bool? allowMergeCommit, bool archived, int watchersCount, bool? deleteBranchOnMerge, RepositoryVisibility visibility, IEnumerable<string> topics, bool? allowAutoMerge, bool? allowUpdateBranch, bool? webCommitSignoffRequired, SecurityAndAnalysis securityAndAnalysis)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            CloneUrl = cloneUrl;
            GitUrl = gitUrl;
            SshUrl = sshUrl;
            SvnUrl = svnUrl;
            MirrorUrl = mirrorUrl;
            ArchiveUrl = archiveUrl;
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
            HasDiscussions = hasDiscussions;
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
#pragma warning disable CS0618 // Type or member is obsolete
            WatchersCount = watchersCount;
#pragma warning restore CS0618 // Type or member is obsolete
            Topics = topics.ToList();
            DeleteBranchOnMerge = deleteBranchOnMerge;
            Visibility = visibility;
            AllowAutoMerge = allowAutoMerge;
            AllowUpdateBranch = allowUpdateBranch;
            WebCommitSignoffRequired = webCommitSignoffRequired;
            SecurityAndAnalysis = securityAndAnalysis;
        }

        public string Url { get; private set; }

        public string HtmlUrl { get; private set; }

        public string CloneUrl { get; private set; }

        public string GitUrl { get; private set; }

        public string SshUrl { get; private set; }

        public string SvnUrl { get; private set; }

        public string MirrorUrl { get; private set; }
        public string ArchiveUrl { get; private set; }

        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public User Owner { get; private set; }

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public bool IsTemplate { get; private set; }

        public string Description { get; private set; }

        public string Homepage { get; private set; }

        public string Language { get; private set; }

        public bool Private { get; private set; }

        public bool Fork { get; private set; }

        public int ForksCount { get; private set; }

        public int StargazersCount { get; private set; }

        [Obsolete("WatchersCount returns the same data as StargazersCount. You are likely looking to use SubscribersCount. Update your code to use SubscribersCount, as this field will stop containing data in the future")]
        public int WatchersCount { get; private set; }

        public string DefaultBranch { get; private set; }

        public int OpenIssuesCount { get; private set; }

        public DateTimeOffset? PushedAt { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public RepositoryPermissions Permissions { get; private set; }

        public Repository Parent { get; private set; }

        public Repository Source { get; private set; }

        public LicenseMetadata License { get; private set; }

        public bool HasDiscussions { get; private set; }

        public bool HasIssues { get; private set; }

        public bool HasWiki { get; private set; }

        public bool HasDownloads { get; private set; }

        public bool? AllowRebaseMerge { get; private set; }

        public bool? AllowSquashMerge { get; private set; }

        public bool? AllowMergeCommit { get; private set; }

        public bool HasPages { get; private set; }

        public int SubscribersCount { get; private set; }

        public long Size { get; private set; }

        public bool Archived { get; private set; }

        public IReadOnlyList<string> Topics { get; private set; }

        public bool? DeleteBranchOnMerge { get; private set; }

        public RepositoryVisibility? Visibility { get; private set; }

        public bool? AllowAutoMerge { get; private set; }

        public bool? AllowUpdateBranch { get; private set; }

        public bool? WebCommitSignoffRequired { get; private set; }

        public SecurityAndAnalysis SecurityAndAnalysis { get; private set;}

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository: Id: {0} Owner: {1}, Name: {2}", Id, Owner?.Login, Name);
            }
        }
    }
}
