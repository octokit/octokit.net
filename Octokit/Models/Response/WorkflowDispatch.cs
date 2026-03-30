using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowDispatch
    {
        public WorkflowDispatch() { }

        public WorkflowDispatch(long workflowRunId, string runUrl, string htmlUrl)
        {
            WorkflowRunId = workflowRunId;
            RunUrl = runUrl;
            HtmlUrl = htmlUrl;
        }

        /// <summary>
        /// The Id for the dispatched workflow run.
        /// </summary>
        public long WorkflowRunId { get; private set; }

        /// <summary>
        /// The API URL for this dispatched workflow.
        /// </summary>
        public string RunUrl { get; private set; }

        /// <summary>
        /// The URL for the HTML view of this dispatched workflow.
        /// </summary>
        public string HtmlUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "WorkflowRunId: {0}", WorkflowRunId);
            }
        }
    }
}
