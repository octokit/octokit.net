using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRun
    {
        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <param name="name">Required. The name of the check. For example, "code-coverage"</param>
        /// <param name="headSha">Required. The SHA of the commit</param>
        public NewCheckRun(string name, string headSha)
        {
            Name = name;
            HeadSha = headSha;
        }

        /// <summary>
        /// Required. The name of the check. For example, "code-coverage"
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Required. The SHA of the commit
        /// </summary>
        public string HeadSha { get; protected set; }

        /// <summary>
        /// The URL of the integrator's site that has the full details of the check
        /// </summary>
        public string DetailsUrl { get; set; }

        /// <summary>
        /// A reference for the run on the integrator's system
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// The current status. Can be one of queued, in_progress, or completed. Default: queued
        /// </summary>
        public StringEnum<CheckStatus>? Status { get; set; }

        /// <summary>
        /// The time that the check run began
        /// </summary>
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Required if you provide completed_at or a status of completed. The final conclusion of the check. Can be one of success, failure, neutral, cancelled, timed_out, or action_required. When the conclusion is action_required, additional details should be provided on the site specified by details_url.
        /// Note: Providing conclusion will automatically set the status parameter to completed
        /// </summary>
        public StringEnum<CheckConclusion>? Conclusion { get; set; }

        /// <summary>
        /// Required if you provide conclusion. The time the check completed
        /// </summary>
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Check runs can accept a variety of data in the output object, including a title and summary and can optionally provide descriptive details about the run
        /// </summary>
        public NewCheckRunOutput Output { get; set; }

        /// <summary>
        /// Possible further actions the integrator can perform, which a user may trigger. Each action includes a label, identifier and description. A maximum of three actions are accepted
        /// </summary>
        public IReadOnlyList<NewCheckRunAction> Actions { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Name: {0}, HeadSha: {1}, Status: {2}, Conclusion: {3}", Name, HeadSha, Status, Conclusion);
    }
}
