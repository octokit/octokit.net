using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsRepos
    {
        public AdminStatsRepos() { }

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

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalRepos: {0} RootRepos: {1} ForkRepos: {2} OrgRepos: {3} TotalPushes: {4} TotalWikis: {5}", TotalRepos, RootRepos, ForkRepos, OrgRepos, TotalPushes, TotalWikis);
            }
        }
    }
}