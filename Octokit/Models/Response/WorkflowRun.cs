using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRun
    {
        public WorkflowRun()
        {
        }

        public WorkflowRun(
            long id, string headBranch, string headSha, int runNumber, long checkSuiteId,
            string @event, CheckStatus status, CheckConclusion? conclusion, string url,
            string htmlUrl, IReadOnlyList<PullRequest> pullRequests, DateTimeOffset createdAt,
            DateTimeOffset updatedAt, HeadCommit headCommit,
            Repository repository, Repository headRepository)
        {
            Id = id;
            HeadBranch = headBranch;
            HeadSha = headSha;
            RunNumber = runNumber;
            CheckSuiteId = checkSuiteId;
            Event = @event;
            Status = status;
            Conclusion = conclusion;
            Url = url;
            HtmlUrl = htmlUrl;
            PullRequests = pullRequests;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            HeadCommit = headCommit;
            Repository = repository;
            HeadRepository = headRepository;
        }

        public long Id { get; protected set; }
        public string HeadBranch { get; protected set; }
        public string HeadSha { get; protected set; }
        public int RunNumber { get; protected set; }
        public long CheckSuiteId { get; protected set; }
        public string Event { get; protected set; }
        public StringEnum<CheckStatus> Status { get; protected set; }
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }
        public string Url { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset UpdatedAt { get; protected set; }
        public HeadCommit HeadCommit { get; protected set; }
        public Repository Repository { get; protected set; }
        public Repository HeadRepository { get; protected set; }

        internal string DebuggerDisplay
            => $"Id: {Id}, HeadBranch: {HeadBranch}, HeadSha: {HeadSha}, CheckSuiteId: {CheckSuiteId}, Conclusion: {Conclusion}";
    }
}
