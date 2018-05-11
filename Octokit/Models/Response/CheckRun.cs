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

        public CheckRun(long id, string headSha, string externalId, string url, string htmlUrl, CheckStatus status, CheckConclusion? conclusion, DateTimeOffset startedAt, DateTimeOffset completedAt, CheckRunOutput output, string name, CheckSuite checkSuite, GitHubApp app, IReadOnlyList<PullRequest> pullRequests)
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

        public long Id { get; protected set; }
        public string HeadSha { get; protected set; }
        public string ExternalId { get; protected set; }
        public string Url { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public StringEnum<CheckStatus> Status { get; protected set; }
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }
        public DateTimeOffset StartedAt { get; protected set; }
        public DateTimeOffset CompletedAt { get; protected set; }
        public CheckRunOutput Output { get; protected set; }
        public string Name { get; protected set; }
        public CheckSuite CheckSuite { get; protected set; }
        public GitHubApp App { get; protected set; }
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, HeadSha: {1}, Conclusion: {2}", Id, HeadSha, Conclusion);
    }
}
