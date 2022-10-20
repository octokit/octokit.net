using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunUsage
    {
        public WorkflowRunUsage() { }

        public WorkflowRunUsage(WorkflowRunBillable billable, long runDurationMs)
        {
            Billable = billable;
            RunDurationMs = runDurationMs;
        }

        /// <summary>
        /// The billable usage.
        /// </summary>
        public WorkflowRunBillable Billable { get; private set; }

        /// <summary>
        /// The total run duration in milliseconds.
        /// </summary>
        public long RunDurationMs { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Billing: {0}", Billable);
            }
        }
    }
}
