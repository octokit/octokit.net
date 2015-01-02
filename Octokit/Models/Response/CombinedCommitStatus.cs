using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CombinedCommitStatus
    {
        /// <summary>
        /// The combined state of the commits.
        /// </summary>
        public CommitState State { get; protected set; }

        /// <summary>
        /// The SHA of the reference.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The total number of statuses.
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// The statuses.
        /// </summary>
        // TODO: This ought to be an IReadOnlyList<ApiErrorDetail> but we need to add support to SimpleJson for that.
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<CommitStatus> Statuses { get; protected set; }

        /// <summary>
        /// The repository of the reference.
        /// </summary>
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SHA: {0}, State: {1}, TotalCount: {2}", Sha, State, TotalCount);
            }
        }
    }
}