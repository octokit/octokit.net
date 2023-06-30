using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowJob
    {
        public WorkflowJob() { }

        public WorkflowJob(long id, long runId, string runUrl, string nodeId, string headSha, string url, string htmlUrl, WorkflowJobStatus status, WorkflowJobConclusion? conclusion, DateTimeOffset? createdAt, DateTimeOffset startedAt, DateTimeOffset? completedAt, string name, IReadOnlyList<WorkflowJobStep> steps, string checkRunUrl, IReadOnlyList<string> labels, long? runnerId = default, string runnerName = default, long? runnerGroupId = default, string runnerGroupName = default)
        {
            Id = id;
            RunId = runId;
            RunUrl = runUrl;
            NodeId = nodeId;
            HeadSha = headSha;
            Url = url;
            HtmlUrl = htmlUrl;
            Status = status;
            Conclusion = conclusion;
            CreatedAt = createdAt;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Name = name;
            Steps = steps;
            CheckRunUrl = checkRunUrl;
            Labels = labels;
            RunnerId = runnerId;
            RunnerName = runnerName;
            RunnerGroupId = runnerGroupId;
            RunnerGroupName = runnerGroupName;
        }

        /// <summary>
        /// The Id of the job.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The Id of the associated workflow run.
        /// </summary>
        public long RunId { get; private set; }

        /// <summary>
        /// The run URL for this job.
        /// </summary>
        public string RunUrl { get; private set; }

        /// <summary>
        /// The SHA of the commit that is being run.
        /// </summary>
        public string HeadSha { get; private set; }

        /// <summary>
        /// The URL for this job.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The URL for the HTML view of this job.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The phase of the lifecycle that the job is currently in.
        /// </summary>
        public StringEnum<WorkflowJobStatus> Status { get; private set; }

        /// <summary>
        /// The outcome of the job.
        /// </summary>
        public StringEnum<WorkflowJobConclusion>? Conclusion { get; private set; }

        /// <summary>
        /// The time that the job was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time that the job started.
        /// </summary>
        public DateTimeOffset StartedAt { get; private set; }

        /// <summary>
        /// The time that the job finished.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; private set; }

        /// <summary>
        /// Name of the workflow job.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Steps in this job.
        /// </summary>
        public IReadOnlyList<WorkflowJobStep> Steps { get; private set; }

        /// <summary>
        /// The URL for the check run for the job.
        /// </summary>
        public string CheckRunUrl { get; private set; }

        /// <summary>
        /// Labels for the workflow job.
        /// </summary>
        public IReadOnlyList<string> Labels { get; private set; }

        /// <summary>
        /// The Id of the runner to which this job has been assigned.
        /// </summary>
        public long? RunnerId { get; private set; }

        /// <summary>
        /// The name of the runner to which this job has been assigned.
        /// </summary>
        public string RunnerName { get; private set; }

        /// <summary>
        /// The Id of the runner group to which this job has been assigned.
        /// </summary>
        public long? RunnerGroupId { get; private set; }

        /// <summary>
        /// The name of the runner group to which this job has been assigned.
        /// </summary>
        public string RunnerGroupName { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name);
            }
        }
    }
}
