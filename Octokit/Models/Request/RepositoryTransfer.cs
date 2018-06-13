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
            Ensure.ArgumentNotNullOrEmptyList(teamId, nameof(teamId));

            TeamId = teamId;
        }

        public string NewOwner { get; set; }

        public IReadOnlyList<int> TeamId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(CultureInfo.InvariantCulture, "NewOwner: {0}", NewOwner);
                strBuilder.Append(", TeamId: ");
                if (TeamId == null) {
                    strBuilder.Append("null");
                    return strBuilder.ToString();
                }

                strBuilder.Append("[ ");
                foreach (int id in TeamId)
                {
                    strBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", id.ToString());
                }
                strBuilder.Append("]");
                return strBuilder.ToString();
            }
        }
    }
}