using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRun
    {
        public CheckRun()
        {
        }

        public CheckRun(long id, string headSha, string externalId, string url, string htmlUrl, CheckStatus status, CheckConclusion? conclusion, DateTimeOffset startedAt, DateTimeOffset? completedAt, CheckRunOutputResponse output, string name, CheckSuite checkSuite, GitHubApp app, IReadOnlyList<PullRequest> pullRequests)
        {
            Id = id;
            HeadSha = headSha;
            ExternalId = externalId;
            Url = url;
            HtmlUrl = htmlUrl;
            Status = status;
            Conclusion = conclusion;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Output = output;
            Name = name;
            CheckSuite = checkSuite;
            App = app;
            PullRequests = pullRequests;
        }

        /// <summary>
        /// The Id of this check run
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The commit this check run is associated with
        /// </summary>
        public string HeadSha { get; protected set; }

        /// <summary>
        /// A reference for the run on the integrator's system.
        /// </summary>
        public string ExternalId { get; protected set; }

        /// <summary>
        /// The GitHub Api URL of this check run
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The GitHub.com URL of this check run
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The check run status
        /// </summary>
        public StringEnum<CheckStatus> Status { get; protected set; }

        /// <summary>
        /// The final conclusion of the check
        /// </summary>
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }

        /// <summary>
        /// The time that the check run began
        /// </summary>
        public DateTimeOffset StartedAt { get; protected set; }

        /// <summary>
        /// The time the check run completed
        /// </summary>
        public DateTimeOffset? CompletedAt { get; protected set; }

        /// <summary>
        /// Descriptive details about the run
        /// </summary>
        public CheckRunOutputResponse Output { get; protected set; }

        /// <summary>
        /// The name of the check
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The check suite that is associated with this check run
        /// </summary>
        public CheckSuite CheckSuite { get; protected set; }

        /// <summary>
        /// The GitHub App that is associated with this check run
        /// </summary>
        public GitHubApp App { get; protected set; }

        /// <summary>
        /// The pull requests that are associated with this check run
        /// </summary>
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, HeadSha: {1}, Conclusion: {2}", Id, HeadSha, Conclusion);
    }
}
