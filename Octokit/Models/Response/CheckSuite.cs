using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuite
    {
        public CheckSuite()
        {
        }

        public CheckSuite(long id, string headBranch, string headSha, CheckStatus status, CheckConclusion? conclusion, string url, string before, string after, IReadOnlyList<PullRequest> pullRequests, GitHubApp app, Repository repository)
        {
            Id = id;
            HeadBranch = headBranch;
            HeadSha = headSha;
            Status = status;
            Conclusion = conclusion;
            Url = url;
            Before = before;
            After = after;
            PullRequests = pullRequests;
            App = app;
            Repository = repository;
        }

        /// <summary>
        /// The Id of this check suite
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The head branch of the commit this check suite is associated with
        /// </summary>
        public string HeadBranch { get; protected set; }

        /// <summary>
        /// The commit this check suite is associated with
        /// </summary>
        public string HeadSha { get; protected set; }

        /// <summary>
        /// The summarized status of the check runs included in this check suite
        /// </summary>
        public StringEnum<CheckStatus> Status { get; protected set; }

        /// <summary>
        /// The summarized conclusion of the check runs included in this check suite
        /// </summary>
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }

        /// <summary>
        /// The GitHub Api URL of this check suite
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The hash of the commit prior to the HeadSha
        /// </summary>
        public string Before { get; protected set; }

        /// <summary>
        /// The hash of the commit after the HeadSha
        /// </summary>
        public string After { get; protected set; }

        /// <summary>
        /// The pull requests that are associated with the check suite (via the HeadSha)
        /// </summary>
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }

        /// <summary>
        /// The GitHub App that is associated with this check suite
        /// </summary>
        public GitHubApp App { get; protected set; }

        /// <summary>
        /// The repository that is associated with this check suite
        /// </summary>
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, HeadBranch: {1}, HeadSha: {2}, Conclusion: {3}", Id, HeadBranch, HeadSha, Conclusion);
    }
}
