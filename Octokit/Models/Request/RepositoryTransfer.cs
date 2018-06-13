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
        /// Creates a new repository transfer description where TeamId is empty.
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
        /// <param name="teamId">A list of team Ids to add to the repository after the transfer.</param>
        /// <remarks>
        /// Teams can only be added to organization-owned repositories, so this constructor
        /// will create an invalid description if that is not the case.
        /// </remarks>
        public RepositoryTransfer(string newOwner, IReadOnlyList<int> teamId)
            : this(newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyEnumerable(teamId, nameof(teamId));

            TeamId = teamId;
        }

        /// <summary>
        /// The new owner of the repository after the transfer.
        /// </summary>
        public string NewOwner { get; set; }

        /// <summary>
        /// A list of team Ids to add to the repository after the transfer.
        /// </summary>
        /// <remarks>
        /// Teams can only be added to organization-owned repositories, so this constructor
        /// will create an invalid description if that is not the case.
        /// </remarks>
        public IReadOnlyList<int> TeamId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                string teamIdStr = string.Join(", ", TeamId ?? new int[0]);
                return string.Format(CultureInfo.InvariantCulture, "NewOwner: {0}, TeamId: [{1}]", NewOwner, teamIdStr);
            }
        }
    }
}