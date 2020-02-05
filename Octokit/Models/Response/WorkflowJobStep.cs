using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowJobStep
    {
        public WorkflowJobStep()
        {
        }

        public WorkflowJobStep(string name, CheckStatus status, CheckConclusion? conclusion, int number, DateTimeOffset startedAt, DateTimeOffset completedAt)
        {
            Name = name;
            Status = status;
            Conclusion = conclusion;
            Number = number;
            StartedAt = startedAt;
            CompletedAt = completedAt;
        }

        public string Name { get; protected set; }

        public StringEnum<CheckStatus> Status { get; protected set; }
        public StringEnum<CheckConclusion>? Conclusion { get; protected set; }
        public int Number { get; protected set; }
        public DateTimeOffset StartedAt { get; protected set; }
        public DateTimeOffset CompletedAt { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0}, Status: {1}, Number: {2}", Name, Status, Number);
    }
}
