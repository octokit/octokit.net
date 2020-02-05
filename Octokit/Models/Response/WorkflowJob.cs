using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowJob
    {
        public WorkflowJob()
        {
        }

        public WorkflowJob(long id, long runId, string headSha, string url, string htmlUrl, CheckStatus status, CheckConclusion? conclusion, DateTimeOffset startedAt, DateTimeOffset completedAt, string name, IReadOnlyList<WorkflowJobStep> steps)
        {
            Id = id;
            RunId = runId;
            HeadSha = headSha;
            Url = url;
            HtmlUrl = htmlUrl;
            Status = status;
            Conclusion = conclusion;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Name = name;
            Steps = steps;
        }

        public long Id { get; protected set; }

        public long RunId { get; protected set; }
        public string HeadSha { get; protected set; }
        public string Url { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public StringEnum<CheckStatus> Status { get; protected set; }
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }
        public DateTimeOffset StartedAt { get; protected set; }
        public DateTimeOffset CompletedAt { get; protected set; }
        public string Name { get; protected set; }
        public IReadOnlyList<WorkflowJobStep> Steps { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}, Status: {2}, Steps: {3}", Id, Name, Status, Steps.Count);

    }
}