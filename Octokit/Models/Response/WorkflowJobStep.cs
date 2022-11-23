using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowJobStep
    {
        public WorkflowJobStep() { }

        public WorkflowJobStep(string name, WorkflowJobStatus status, WorkflowJobConclusion conclusion, int number, DateTimeOffset? startedAt, DateTimeOffset? completedAt)
        {
            Name = name;
            Status = status;
            Conclusion = conclusion;
            Number = number;
            StartedAt = startedAt;
            CompletedAt = completedAt;
        }

        /// <summary>
        /// The name of the step.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The number of the step.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// The phase of the lifecycle that the job is currently in.
        /// </summary>
        public StringEnum<WorkflowJobStatus> Status { get; private set; }

        /// <summary>
        /// The outcome of the job.
        /// </summary>
        public StringEnum<WorkflowJobConclusion>? Conclusion { get; private set; }

        /// <summary>
        /// The time that the step started.
        /// </summary>
        public DateTimeOffset? StartedAt { get; private set; }

        /// <summary>
        /// The time that the step finished.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Number: {0} Name: {1}", Number, Name);
            }
        }
    }
}
