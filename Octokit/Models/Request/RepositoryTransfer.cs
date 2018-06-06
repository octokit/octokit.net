using System;
using System.Text;
using System.Diagnostics;
using System.Globalization;

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

        public RepositoryTransfer(string newOwner, int[] teamId)
            : this(newOwner)
        {
            Ensure.ArgumentNotNullOrEmptyArray(teamId, nameof(teamId));

            TeamId = new int[teamId.Length];
            Array.Copy(teamId, TeamId, teamId.Length);
        }

        public string NewOwner { get; set; }

        public int[] TeamId { get; set; }

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