using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class CombinedCommitStatus
    {
        /// <summary>
        /// The combined state of the commits.
        /// </summary>
        public CommitState State { get; set; }

        /// <summary>
        /// The SHA of the reference.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The total number of statuses.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The statuses.
        /// </summary>
        // TODO: This ought to be an IReadOnlyList<ApiErrorDetail> but we need to add support to SimpleJson for that.
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<CommitStatus> Statuses { get; set; }

        /// <summary>
        /// The repository of the reference.
        /// </summary>
        public Repository Repository { get; set; }
    }
}