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

        public CheckRun(long id, string headSha, string externalId, string url, string htmlUrl, string detailsUrl, CheckStatus status, CheckConclusion? conclusion, DateTimeOffset startedAt, DateTimeOffset? completedAt, CheckRunOutputResponse output, string name, CheckSuite checkSuite, GitHubApp app, IReadOnlyList<PullRequest> pullRequests)
        {
            Id = id;
            HeadSha = headSha;
            ExternalId = externalId;
            Url = url;
            HtmlUrl = htmlUrl;
            DetailsUrl = detailsUrl;
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
        /// The Id of the check run
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The SHA of the commit the check run is associated with
        /// </summary>
        public string HeadSha { get; private set; }

        /// <summary>
        /// A reference for the run on the integrator's system
        /// </summary>
        public string ExternalId { get; private set; }

        /// <summary>
        /// The GitHub API URL of the check run
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The GitHub.com URL of the check run
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The URL of the integrator's site that has the full details of the check.
        /// </summary>
        public string DetailsUrl { get; private set; }

        /// <summary>
        /// The check run status
        /// </summary>
        public StringEnum<CheckStatus> Status { get; private set; }

        /// <summary>
        /// The final conclusion of the check
        /// </summary>
        public StringEnum<CheckConclusion>? Conclusion { get; private set; }

        /// <summary>
        /// The time that the check run began
        /// </summary>
        public DateTimeOffset StartedAt { get; private set; }

        /// <summary>
        /// The time the check run completed
        /// </summary>
        public DateTimeOffset? CompletedAt { get; private set; }

        /// <summary>
        /// Descriptive details about the run
        /// </summary>
        public CheckRunOutputResponse Output { get; private set; }

        /// <summary>
        /// The name of the check
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The check suite that is associated with this check run
        /// </summary>
        public CheckSuite CheckSuite { get; private set; }

        /// <summary>
        /// The GitHub App that is associated with this check run
        /// </summary>
        public GitHubApp App { get; private set; }

        /// <summary>
        /// The pull requests that are associated with this check run
        /// </summary>
        public IReadOnlyList<PullRequest> PullRequests { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, HeadSha: {1}, Conclusion: {2}", Id, HeadSha, Conclusion);
    }
}
