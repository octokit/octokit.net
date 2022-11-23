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

        public WorkflowRun(long id, string name, string nodeId, long checkSuiteId, string checkSuiteNodeId, string headBranch, string headSha, string path, long runNumber, string @event, string displayTitle, WorkflowRunStatus status, WorkflowRunConclusion? conclusion, long workflowId, string url, string htmlUrl, IReadOnlyList<PullRequest> pullRequests, DateTimeOffset createdAt, DateTimeOffset updatedAt, User actor, long runAttempt, IReadOnlyList<WorkflowReference> referencedWorkflows, DateTimeOffset runStartedAt, User triggeringActor, string jobsUrl, string logsUrl, string checkSuiteUrl, string artifactsUrl, string cancelUrl, string rerunUrl, string previousAttemptUrl, string workflowUrl, Commit headCommit, Repository repository, Repository headRepository, long headRepositoryId)
        {
            Id = id;
            Name = name;
            NodeId = nodeId;
            CheckSuiteId = checkSuiteId;
            CheckSuiteNodeId = checkSuiteNodeId;
            HeadBranch = headBranch;
            HeadSha = headSha;
            Path = path;
            RunNumber = runNumber;
            Event = @event;
            DisplayTitle = displayTitle;
            Status = status;
            Conclusion = conclusion;
            WorkflowId = workflowId;
            Url = url;
            HtmlUrl = htmlUrl;
            PullRequests = pullRequests;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Actor = actor;
            RunAttempt = runAttempt;
            ReferencedWorkflows = referencedWorkflows;
            RunStartedAt = runStartedAt;
            TriggeringActor = triggeringActor;
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
        public long Id { get; private set; }

        /// <summary>
        /// The name of the workflow run.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The ID of the associated check suite.
        /// </summary>
        public long CheckSuiteId { get; private set; }

        /// <summary>
        /// The node ID of the associated check suite.
        /// </summary>
        public string CheckSuiteNodeId { get; private set; }

        /// <summary>
        /// The head branch.
        /// </summary>
        public string HeadBranch { get; private set; }

        /// <summary>
        /// The SHA of the head commit that points to the version of the workflow being run.
        /// </summary>
        public string HeadSha { get; private set; }

        /// <summary>
        /// The full path of the workflow.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The auto incrementing run number for the workflow run.
        /// </summary>
        public long RunNumber { get; private set; }

        /// <summary>
        /// The event that triggered the workflow run.
        /// </summary>
        public string Event { get; private set; }

        /// <summary>
        /// The event-specific title associated with the run or the run-name if set.
        /// </summary>
        public string DisplayTitle { get; private set; }

        /// <summary>
        /// The status of the the workflow run.
        /// </summary>
        public StringEnum<WorkflowRunStatus> Status { get; private set; }

        /// <summary>
        /// The conclusion of the the workflow run.
        /// </summary>
        public StringEnum<WorkflowRunConclusion>? Conclusion { get; private set; }

        /// <summary>
        /// The ID of the parent workflow.
        /// </summary>
        public long WorkflowId { get; private set; }

        /// <summary>
        /// The URL for this workflow run.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The URL for the HTML view of this workflow run.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// Any associated pull requests.
        /// </summary>
        public IReadOnlyList<PullRequest> PullRequests { get; private set; }

        /// <summary>
        /// The time that the workflow run was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The time that the workflow run was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// The actor associated with the workflow run.
        /// </summary>
        public User Actor { get; private set; }

        /// <summary>
        /// Attempt number of the run, 1 for first attempt and higher if the workflow was re-run.
        /// </summary>
        public long RunAttempt { get; private set; }

        /// <summary>
        /// Any associated pull requests.
        /// </summary>
        public IReadOnlyList<WorkflowReference> ReferencedWorkflows { get; private set; }

        /// <summary>
        /// The time that the workflow run started.
        /// </summary>
        public DateTimeOffset RunStartedAt { get; private set; }

        /// <summary>
        /// The actor that triggered the workflow run.
        /// </summary>
        public User TriggeringActor { get; private set; }

        /// <summary>
        /// The URL for this workflow run's job.
        /// </summary>
        public string JobsUrl { get; private set; }

        /// <summary>
        /// The URL for this workflow run's logs.
        /// </summary>
        public string LogsUrl { get; private set; }

        /// <summary>
        /// The URL for this workflow run's check suite.
        /// </summary>
        public string CheckSuiteUrl { get; private set; }

        /// <summary>
        /// The URL for this workflow run's artifacts.
        /// </summary>
        public string ArtifactsUrl { get; private set; }

        /// <summary>
        /// The URL to cancel this workflow run.
        /// </summary>
        public string CancelUrl { get; private set; }

        /// <summary>
        /// The URL to re-run this workflow run.
        /// </summary>
        public string RerunUrl { get; private set; }

        /// <summary>
        /// The URL for this workflow run's previous run.
        /// </summary>
        public string PreviousAttemptUrl { get; private set; }

        /// <summary>
        /// The URL for this workflow run's workflow.
        /// </summary>
        public string WorkflowUrl { get; private set; }

        /// <summary>
        /// The head commit of the workflow run.
        /// </summary>
        public Commit HeadCommit { get; private set; }

        /// <summary>
        /// The repository associated with the workflow run.
        /// </summary>
        public Repository Repository { get; private set; }

        /// <summary>
        /// The head repository associated with the workflow run.
        /// </summary>
        public Repository HeadRepository { get; private set; }

        /// <summary>
        /// The ID of the head repository.
        /// </summary>
        public long HeadRepositoryId { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name);
            }
        }
    }
}
