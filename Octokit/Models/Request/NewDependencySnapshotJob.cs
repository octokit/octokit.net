using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshotJob
    {
        /// <summary>
        /// Creates a new Dependency Snapshot Job.
        /// </summary>
        /// <param name="id">Required. The external ID of the job.</param>
        /// <param name="correlator">Required. Correlator that is used to group snapshots submitted over time.</param>
        public NewDependencySnapshotJob(string id, string correlator)
        {
            Ensure.ArgumentNotNullOrEmptyString(correlator, nameof(correlator));

            Id = id;
            Correlator = correlator;
        }

        /// <summary>
        /// Required. The external ID of the job.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Required. Correlator that is used to group snapshots submitted over time.
        /// </summary>
        public string Correlator { get; private set; }

        /// <summary>
        /// Optional. The URL for the job.
        /// </summary>
        public string HtmlUrl { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, HtmlUrl: {1}", Id, HtmlUrl);
            }
        }
    }
}
