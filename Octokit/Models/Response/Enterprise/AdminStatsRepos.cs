namespace Octokit
{
    public class AdminStatsRepos
    {
        public AdminStatsRepos(int totalRepos, int rootRepos, int forkRepos, int orgRepos, int totalPushes, int totalWikis)
        {
            TotalRepos = totalRepos;
            RootRepos = rootRepos;
            ForkRepos = forkRepos;
            OrgRepos = orgRepos;
            TotalPushes = totalPushes;
            TotalWikis = totalWikis;
        }

        public int TotalRepos
        {
            get;
            private set;
        }

        public int RootRepos
        {
            get;
            private set;
        }

        public int ForkRepos
        {
            get;
            private set;
        }

        public int OrgRepos
        {
            get;
            private set;
        }

        public int TotalPushes
        {
            get;
            private set;
        }

        public int TotalWikis
        {
            get;
            private set;
        }
    }
}