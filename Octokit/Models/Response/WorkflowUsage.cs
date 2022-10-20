using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowUsage
    {
        public WorkflowUsage() { }

        public WorkflowUsage(WorkflowBillable billable)
        {
            Billable = billable;
        }

        /// <summary>
        /// The billable usage.
        /// </summary>
        public WorkflowBillable Billable { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Billing: {0}", Billable);
            }
        }
    }
}
