using System;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTransfer
    {
        public RepositoryTransfer(string newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyString(newOwner, nameof(newOwner));

            NewOwner = newOwner;
        }

        public RepositoryTransfer(string newOwner, IReadOnlyList<int> teamId)
            : this(newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyEnumerable(teamId, nameof(teamId));

            TeamId = teamId;
        }

        public string NewOwner { get; set; }

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