using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequest
    {
        /// <summary>
        /// The URL for this pull request.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The URL for the pull request page.
        /// </summary>
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// The URL for the pull request's diff (.diff) file.
        /// </summary>
        public Uri DiffUrl { get; set; }

        /// <summary>
        /// The URL for the pull request's patch (.patch) file.
        /// </summary>
        public Uri PatchUrl { get; set; }

        /// <summary>
        /// The URL for the specific pull request issue.
        /// </summary>
        public Uri IssueUrl { get; set; }

        /// <summary>
        /// The URL for the pull request statuses.
        /// </summary>
        public Uri StatusesUrl { get; set; }

        /// <summary>
        /// The pull request number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// Title of the pull request.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The body (content) contained within the pull request.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// When the pull request was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When the pull request was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// When the pull request was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// When the pull request was merged.
        /// </summary>
        public DateTimeOffset? MergedAt { get; set; }

        /// <summary>
        /// The HEAD reference for the pull request.
        /// </summary>
        public GitReference Head { get; set; }

        /// <summary>
        /// The BASE reference for the pull request.
        /// </summary>
        public GitReference Base { get; set; }
        
        /// <summary>
        /// The user who created the pull request.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The SHA of the merge commit.
        /// </summary>
        public string MergeCommitSha { get; set; }

        /// <summary>
        /// Whether or not the pull request has been merged.
        /// </summary>
        public bool Merged { get; set; }

        /// <summary>
        /// Whether or not the pull request can be merged.
        /// </summary>
        public bool Mergable { get; set; }

        /// <summary>
        /// The user who merged the pull request.
        /// </summary>
        public User MergedBy { get; set; }

        /// <summary>
        /// Total number of comments contained in the pull request.
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// Total number of commits contained in the pull request.
        /// </summary>
        public int Commits { get; set; }

        /// <summary>
        /// Total number of additions contained in the pull request.
        /// </summary>
        public int Additions { get; set; }

        /// <summary>
        /// Total number of deletions contained in the pull request.
        /// </summary>
        public int Deletions { get; set; }

        /// <summary>
        /// Total number of files changed in the pull request.
        /// </summary>
        public int ChangedFiles { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Number: {0} State: {1}", Number, State);
            }
        }
    }
}