using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunTiming
    {
        public WorkflowRunTiming() { }

        public WorkflowRunTiming(long jobId, long durationMs)
        {
            JobId = jobId;
            DurationMs = durationMs;
        }

        /// <summary>
        /// The job Id.
        /// </summary>
        public long JobId { get; private set; }

        /// <summary>
        /// The duration of the job in milliseconds.
        /// </summary>
        public long DurationMs { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "JobId: {0}, DurationMs: {1}", JobId, DurationMs);
            }
        }
    }
}
