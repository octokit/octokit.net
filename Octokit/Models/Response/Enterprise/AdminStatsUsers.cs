using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsUsers
    {
        public AdminStatsUsers() { }

        public AdminStatsUsers(int totalUsers, int adminUsers, int suspendedUsers)
        {
            TotalUsers = totalUsers;
            AdminUsers = adminUsers;
            SuspendedUsers = suspendedUsers;
        }

        public int TotalUsers
        {
            get;
            private set;
        }

        public int AdminUsers
        {
            get;
            private set;
        }

        public int SuspendedUsers
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalUsers: {0} AdminUsers: {1} SuspendedUsers: {2}", TotalUsers, AdminUsers, SuspendedUsers);
            }
        }
    }
}