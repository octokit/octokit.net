namespace Octokit
{
    public class AdminStatsOrgs
    {
        public AdminStatsOrgs() { }

        public AdminStatsOrgs(int totalOrgs, int disabledOrgs, int totalTeams, int totalTeamMembers)
        {
            TotalOrgs = totalOrgs;
            DisabledOrgs = disabledOrgs;
            TotalTeams = totalTeams;
            TotalTeamMembers = totalTeamMembers;
        }

        public int TotalOrgs
        {
            get;
            private set;
        }

        public int DisabledOrgs
        {
            get;
            private set;
        }

        public int TotalTeams
        {
            get;
            private set;
        }

        public int TotalTeamMembers
        {
            get;
            private set;
        }
    }
}