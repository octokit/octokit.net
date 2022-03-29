using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRun
    {
        public WorkflowRun() { }

        public WorkflowRun(long id, string name, string nodeId, long checkSuiteId, string checkSuiteNodeId, string headBranch, string headSha, long runNumber, long runAttempt, string @event, WorkflowRunStatus status, WorkflowRunConclusion conclusion, long workflowId, string url, string htmlUrl, IReadOnlyList<PullRequest> pullRequests, DateTimeOffset createdAt, DateTimeOffset updatedAt, User actor, User triggeringActor, DateTimeOffset runStartedAt, string jobsUrl, string logsUrl, string checkSuiteUrl, string artifactsUrl, string cancelUrl, string rerunUrl, string previousAttemptUrl, string workflowUrl, Commit headCommit, Repository repository, Repository headRepository, long headRepositoryId)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            CheckSuiteId = checkSuiteId;
            CheckSuiteNodeId = checkSuiteNodeId;
            HeadBranch = headBranch;
            HeadSha = headSha;
            RunNumber = runNumber;
            RunAttempt = runAttempt;
            Event = @event;
            Status = status;
            Conclusion = conclusion;
            WorkflowId = workflowId;
            Url = url;
            HtmlUrl = htmlUrl;
            PullRequests = pullRequests;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Actor = actor;
            TriggeringActor = triggeringActor;
            RunStartedAt = runStartedAt;
            JobsUrl = jobsUrl;
            LogsUrl = logsUrl;
            CheckSuiteUrl = checkSuiteUrl;
            ArtifactsUrl = artifactsUrl;
            CancelUrl = cancelUrl;
            RerunUrl = rerunUrl;
            PreviousAttemptUrl = previousAttemptUrl;
            WorkflowUrl = workflowUrl;
            HeadCommit = headCommit;
            Repository = repository;
            HeadRepository = headRepository;
            HeadRepositoryId = headRepositoryId;
        }

        /// <summary>
        /// The ID of the workflow run.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The name of the workflow run.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The ID of the associated check suite.
        /// </summary>
        public long CheckSuiteId { get; protected set; }

        /// <summary>
        /// The node ID of the associated check suite.
        /// </summary>
        public string CheckSuiteNodeId { get; protected set; }

        /// <summary>
        /// The head branch.
        /// </summary>
        public string HeadBranch { get; protected set; }

        /// <summary>
        /// The SHA of the head commit that points to the version of the workflow being run.
        /// </summary>
        public string HeadSha { get; protected set; }

        /// <summary>
        /// The auto incrementing run number for the workflow run.
        /// </summary>
        public long RunNumber { get; protected set; }

        /// <summary>
        /// Attempt number of the run, 1 for first attempt and higher if the workflow was re-run.
        /// </summary>
        public long RunAttempt { get; protected set; }

        /// <summary>
        /// The event that triggered the workflow run.
        /// </summary>
        public string Event { get; protected set; }

        /// <summary>
        /// The status of the the workflow run.
        /// </summary>
        public StringEnum<WorkflowRunStatus> Status { get; protected set; }

        /// <summary>
        /// The conclusion of the the workflow run.
        /// </summary>
        public StringEnum<WorkflowRunConclusion> Conclusion { get; protected set; }

        /// <summary>
        /// The ID of the parent workflow.
        /// </summary>
        public long WorkflowId { get; protected set; }

        /// <summary>
        /// The URL for this workflow run.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The URL for the HTML view of this workflow run.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// Any associated pull requests.
        /// </summary>
        public IReadOnlyList<PullRequest> PullRequests { get; protected set; }

        /// <summary>
        /// The time that the workflow run was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The time that the workflow run was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// The actor associated with the workflow run.
        /// </summary>
        public User Actor { get; protected set; }

        /// <summary>
        /// The actor that triggered the workflow run.
        /// </summary>
        public User TriggeringActor { get; protected set; }

        /// <summary>
        /// The time that the workflow run started.
        /// </summary>
        public DateTimeOffset RunStartedAt { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's job.
        /// </summary>
        public string JobsUrl { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's logs.
        /// </summary>
        public string LogsUrl { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's check suite.
        /// </summary>
        public string CheckSuiteUrl { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's artifacts.
        /// </summary>
        public string ArtifactsUrl { get; protected set; }

        /// <summary>
        /// The URL to cancel this workflow run.
        /// </summary>
        public string CancelUrl { get; protected set; }

        /// <summary>
        /// The URL to re-run this workflow run.
        /// </summary>
        public string RerunUrl { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's previous run.
        /// </summary>
        public string PreviousAttemptUrl { get; protected set; }

        /// <summary>
        /// The URL for this workflow run's workflow.
        /// </summary>
        public string WorkflowUrl { get; protected set; }

        /// <summary>
        /// The head commit of the workflow run.
        /// </summary>
        public Commit HeadCommit { get; protected set; }

        /// <summary>
        /// The repository associated with the workflow run.
        /// </summary>
        public Repository Repository { get; protected set; }

        /// <summary>
        /// The head repository associated with the workflow run.
        /// </summary>
        public Repository HeadRepository { get; protected set; }

        /// <summary>
        /// The ID of the head repository.
        /// </summary>
        public long HeadRepositoryId { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name);
            }
        }
    }
}
