using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A teams's repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamRepository
    {
        public TeamRepository() { }

        public TeamRepository(long id,
            string nodeId,
            string name,
            string fullName,
            LicenseMetadata license,
            TeamRepositoryPermissions permissions,
            string roleName,
            User owner,
            bool @private,
            string htmlUrl,
            string description,
            bool fork,
            string url,
            string archiveUrl,
            string assigneesUrl,
            string blobsUrl,
            string branchesUrl,
            string collaboratorsUrl,
            string commentsUrl,
            string commitsUrl,
            string compareUrl,
            string contentsUrl,
            string contributorsUrl,
            string deploymentsUrl,
            string downloadsUrl,
            string eventsUrl,
            string forksUrl,
            string gitCommitUrl,
            string gitRefsUrl,
            string gitTagsUrl,
            string gitUrl,
            string issueCommentUrl,
            string issueEventsUrl,
            string issuesUrl,
            string keysUrl,
            string labelsUrl,
            string languagesUrl,
            string mergesUrl,
            string milestonesUrl,
            string notificationsUrl,
            string pullsUrl,
            string releasesUrl,
            string sshUrl,
            string stargazersUrl,
            string statusesUrl,
            string subscribersUrl,
            string subscriptionUrl,
            string tagsUrl,
            string teamsUrl,
            string treesUrl,
            string cloneUrl,
            string mirrorUrl,
            string hooksUrl,
            string svnUrl,
            string homePage,
            string language,
            int forksCount,
            int stargazersCount,
            int watchersCount,
            int size,
            string defaultBranch,
            int openIssuesCount,
            bool isTemplate,
            IReadOnlyList<string> topics,
            bool hasIssues,
            bool hasProjects,
            bool hasWiki,
            bool hasPages,
            bool hasDownloads,
            bool archived,
            bool disabled,
            RepositoryVisibility? visibility,
            DateTimeOffset? pushedAt,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt,
            bool? allowRebaseMerge,
            Repository templateRepository,
            string tempCloneToken,
            bool? allowSquashMerge,
            bool? allowAutoMerge,
            bool? deleteBranchOnMerge,
            bool? allowMergeCommit,
            bool? allowForking,
            bool? webCommitSignoffRequired,
            int subscribersCount,
            int networkCount,
            int openIssues,
            int watchers,
            string masterBranch)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            FullName = fullName;
            License = license;
            Permissions = permissions;
            RoleName = roleName;
            Owner = owner;
            Private = @private;
            HtmlUrl = htmlUrl;
            Description = description;
            Fork = fork;
            Url = url;
            ArchiveUrl = archiveUrl;
            AssigneesUrl = assigneesUrl;
            BlobsUrl = blobsUrl;
            BranchesUrl = branchesUrl;
            CollaboratorsUrl = collaboratorsUrl;
            CommentsUrl = commentsUrl;
            CommitsUrl = commitsUrl;
            CompareUrl = compareUrl;
            ContentsUrl = contentsUrl;
            ContributorsUrl = contributorsUrl;
            DeploymentsUrl = deploymentsUrl;
            DownloadsUrl = downloadsUrl;
            EventsUrl = eventsUrl;
            ForksUrl = forksUrl;
            GitCommitUrl = gitCommitUrl;
            GitRefsUrl = gitRefsUrl;
            GitTagsUrl = gitTagsUrl;
            GitUrl = gitUrl;
            IssueCommentUrl = issueCommentUrl;
            IssueEventsUrl = issueEventsUrl;
            IssuesUrl = issuesUrl;
            KeysUrl = keysUrl;
            LabelsUrl = labelsUrl;
            LanguagesUrl = languagesUrl;
            MergesUrl = mergesUrl;
            MilestonesUrl = milestonesUrl;
            NotificationsUrl = notificationsUrl;
            PullsUrl = pullsUrl;
            ReleasesUrl = releasesUrl;
            SshUrl = sshUrl;
            StargazersUrl = stargazersUrl;
            StatusesUrl = statusesUrl;
            SubscribersUrl = subscribersUrl;
            SubscriptionUrl = subscriptionUrl;
            TagsUrl = tagsUrl;
            TeamsUrl = teamsUrl;
            TreesUrl = treesUrl;
            CloneUrl = cloneUrl;
            MirrorUrl = mirrorUrl;
            HooksUrl = hooksUrl;
            SvnUrl = svnUrl;
            HomePage = homePage;
            Language = language;
            ForksCount = forksCount;
            StargazersCount = stargazersCount;
            WatchersCount = watchersCount;
            Size = size;
            DefaultBranch = defaultBranch;
            OpenIssuesCount = openIssuesCount;
            IsTemplate = isTemplate;
            Topics = topics;
            HasIssues = hasIssues;
            HasProjects = hasProjects;
            HasWiki = hasWiki;
            HasPages = hasPages;
            HasDownloads = hasDownloads;
            Archived = archived;
            Disabled = disabled;
            Visibility = visibility;
            PushedAt = pushedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AllowRebaseMerge = allowRebaseMerge;
            TemplateRepository = templateRepository;
            TempCloneToken = tempCloneToken;
            AllowSquashMerge = allowSquashMerge;
            AllowAutoMerge = allowAutoMerge;
            DeleteBranchOnMerge = deleteBranchOnMerge;
            AllowMergeCommit = allowMergeCommit;
            AllowForking = allowForking;
            WebCommitSignoffRequired = webCommitSignoffRequired;
            SubscribersCount = subscribersCount;
            NetworkCount = networkCount;
            OpenIssues = openIssues;
            Watchers = watchers;
            MasterBranch = masterBranch;
        }


        /// <summary>
        /// Unique identifier of the repository
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The name of the repository
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// example: octocat/Hello-World
        /// </summary>
        public string FullName { get; private set; }

        public LicenseMetadata License { get; private set; }

        public TeamRepositoryPermissions Permissions { get; private set; }

        public string RoleName { get; private set; }

        public User Owner { get; private set; }

        /// <summary>
        /// hether the repository is private or public.
        /// default: false
        /// </summary>
        public bool Private { get; private set; }

        /// <summary>
        /// format: uri
        /// example: https://github.com/octocat/Hello-World
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// example: This your first repo!
        /// nullable: true
        /// </summary>
        public string Description { get; private set; }

        public bool Fork { get; private set; }

        /// <summary>
        /// format: uri
        /// example: https://api.github.com/repos/octocat/Hello-World
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/{archive_format}{/ref}
        /// </summary>
        public string ArchiveUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/assignees{/user}
        /// </summary>
        public string AssigneesUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/git/blobs{/sha}
        /// </summary>
        public string BlobsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/branches{/branch}
        /// </summary>
        public string BranchesUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/collaborators{/collaborator}
        /// </summary>
        public string CollaboratorsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/comments{/number}
        /// </summary>
        public string CommentsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/commits{/sha}
        /// </summary>
        public string CommitsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/compare/{base}...{head}
        /// </summary>
        public string CompareUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/contents/{+path}
        /// </summary>
        public string ContentsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/contributors
        /// </summary>
        public string ContributorsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/deployments
        /// </summary>
        public string DeploymentsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/downloads
        /// </summary>
        public string DownloadsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/events
        /// </summary>
        public string EventsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/forks
        /// </summary>
        public string ForksUrl { get; private set; }
        ///example: http://api.github.com/repos/octocat/Hello-World/git/commits{/sha}
        public string GitCommitUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/git/refs{/sha}
        /// </summary>
        public string GitRefsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/git/tags{/sha}
        /// </summary>
        public string GitTagsUrl { get; private set; }

        /// <summary>
        /// example: git:github.com/octocat/Hello-World.git
        /// </summary>
        public string GitUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/issues/comments{/number}
        /// </summary>
        public string IssueCommentUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/issues/events{/number}
        /// </summary>
        public string IssueEventsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/issues{/number}
        /// </summary>
        public string IssuesUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/keys{/keyId}
        /// </summary>
        public string KeysUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/labels{/name}
        /// </summary>
        public string LabelsUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/languages
        /// </summary>
        public string LanguagesUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/merges
        /// </summary>
        public string MergesUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/milestones{/number}
        /// </summary>
        public string MilestonesUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/notifications{?since,all,participating}
        /// </summary>
        public string NotificationsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/pulls{/number}
        /// </summary>
        public string PullsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/releases{/id}
        /// </summary>
        public string ReleasesUrl { get; private set; }

        /// <summary>
        /// example: git @github.com:octocat/Hello-World.git
        ///
        /// </summary>
        public string SshUrl { get; private set; }

        /// <summary>
        ///  format: uri
        ///  example: http://api.github.com/repos/octocat/Hello-World/stargazers
        /// </summary>
        public string StargazersUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/statuses/{sha}
        /// </summary>
        public string StatusesUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/subscribers
        /// </summary>
        public string SubscribersUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/subscription
        /// </summary>
        public string SubscriptionUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/tags
        /// </summary>
        public string TagsUrl { get; private set; }


        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/teams
        /// </summary>
        public string TeamsUrl { get; private set; }

        /// <summary>
        /// example: http://api.github.com/repos/octocat/Hello-World/git/trees{/sha}
        /// </summary>
        public string TreesUrl { get; private set; }

        /// <summary>
        /// example: https://github.com/octocat/Hello-World.git
        /// </summary>
        public string CloneUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: git:git.example.com/octocat/Hello-World
        /// nullable: true
        /// </summary>
        public string MirrorUrl { get; private set; }


        /// <summary>
        /// format: uri
        /// example: http://api.github.com/repos/octocat/Hello-World/hooks
        /// </summary>
        public string HooksUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: https://svn.github.com/octocat/Hello-World
        /// </summary>
        public string SvnUrl { get; private set; }

        /// <summary>
        /// format: uri
        /// example: https://github.com
        /// </summary>
        public string HomePage { get; private set; }

        public string Language { get; private set; }

        public int ForksCount { get; private set; }

        public int StargazersCount { get; private set; }

        public int WatchersCount { get; private set; }

        public int Size { get; private set; }

        /// <summary>
        /// he default branch of the repository.
        /// example: master
        /// </summary>
        public string DefaultBranch { get; private set; }

        public int OpenIssuesCount { get; private set; }

        /// <summary>
        /// Whether this repository acts as a template that can be used to generate new repositories
        /// default: false
        /// </summary>
        public bool IsTemplate { get; private set; }

        public IReadOnlyList<string> Topics { get; private set; }

        /// <summary>
        /// Whether issues are enabled.
        /// </summary>
        public bool HasIssues { get; private set; }

        /// <summary>
        /// Whether projects are enabled.
        /// </summary>
        public bool HasProjects { get; private set; }

        /// <summary>
        /// Whether the wiki is enabled
        /// </summary>
        public bool HasWiki { get; private set; }

        public bool HasPages { get; private set; }

        /// <summary>
        /// Whether downloads are enabled
        /// default: true
        /// </summary>
        public bool HasDownloads { get; private set; }

        /// <summary>
        /// Whether the repository is archived
        /// </summary>
        public bool Archived { get; private set; }

        /// <summary>
        /// Returns whether or not this repository disabled.
        /// </summary>
        public bool Disabled { get; private set; }

        /// <summary>
        /// The repository visibility: public, private, or internal.
        /// </summary>
        public RepositoryVisibility? Visibility { get; private set; }

        /// <summary>
        /// format: date-time
        /// example: '2011-01-26T19:06:43Z'
        /// </summary>
        public DateTimeOffset? PushedAt { get; private set; }

        /// <summary>
        /// format: date-time
        /// example: '2011-01-26T19:01:12Z'
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// format: date-time
        /// example: '2011-01-26T19:14:43Z'
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// Whether to allow rebase merges for pull requests.
        /// </summary>
        public bool? AllowRebaseMerge { get; private set; }

        /// <summary>
        /// Template repository (nullable)
        /// </summary>
        public Repository TemplateRepository { get; private set; }

        public string TempCloneToken { get; private set; }

        /// <summary>
        /// Whether to allow squash merges for pull requests.
        /// </summary>
        public bool? AllowSquashMerge { get; private set; }

        /// <summary>
        /// Whether to allow Auto-merge to be used on pull requests.
        /// </summary>
        public bool? AllowAutoMerge { get; private set; }

        /// <summary>
        /// Whether to delete head branches when pull requests are merged
        /// </summary>
        public bool? DeleteBranchOnMerge { get; private set; }

        /// <summary>
        /// hether to allow merge commits for pull requests.
        /// </summary>
        public bool? AllowMergeCommit { get; private set; }

        /// <summary>
        /// Whether to allow forking this repo
        /// </summary>
        public bool? AllowForking { get; private set; }

        /// <summary>
        /// Whether to require contributors to sign off on web-based commits
        /// </summary>
        public bool? WebCommitSignoffRequired { get; private set; }

        public int SubscribersCount { get; private set; }

        public int NetworkCount { get; private set; }

        public int OpenIssues { get; private set; }

        public int Watchers { get; private set; }

        public string MasterBranch { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} ", FullName); }
        }
    }
}
