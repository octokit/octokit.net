namespace Octokit
{
    public class AdminStatsUsers
    {
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
    }
}