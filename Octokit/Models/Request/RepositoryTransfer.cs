using System;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Describes the transfer of a repository to a new owner.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTransfer
    {
        /// <summary>
        /// Creates a new repository transfer description with no team Ids.
        /// </summary>
        /// <param name="newOwner">The new owner of the repository after the transfer.</param>
        public RepositoryTransfer(string newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyString(newOwner, nameof(newOwner));

            NewOwner = newOwner;
        }

        /// <summary>
        /// Creates a new repository transfer description.
        /// </summary>
        /// <param name="newOwner">The new owner of the repository after the transfer.</param>
        /// <param name="teamIds">A list of team Ids to add to the repository after the transfer (only applies to transferring to an Organization).</param>
        public RepositoryTransfer(string newOwner, IReadOnlyList<int> teamIds)
            : this(newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyEnumerable(teamIds, nameof(teamIds));

            TeamIds = teamIds;
        }

        /// <summary>
        /// The new owner of the repository after the transfer.
        /// </summary>
        public string NewOwner { get; set; }

        /// <summary>
        /// A list of team Ids to add to the repository after the transfer (only applies to transferring to an Organization).
        /// </summary>
        public IReadOnlyList<int> TeamIds { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                string teamIdsStr = string.Join(", ", TeamIds ?? new int[0]);
                return string.Format(CultureInfo.InvariantCulture, "NewOwner: {0}, TeamIds: [{1}]", NewOwner, teamIdsStr);
            }
        }
    }
}