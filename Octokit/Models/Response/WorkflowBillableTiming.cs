using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowBillableTiming
    {
        public WorkflowBillableTiming() { }

        public WorkflowBillableTiming(long totalMs)
        {
            TotalMs = totalMs;
        }

        /// <summary>
        /// The total billable milliseconds.
        /// </summary>
        public long TotalMs { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "TotalMs: {0}", TotalMs);
            }
        }
    }
}
