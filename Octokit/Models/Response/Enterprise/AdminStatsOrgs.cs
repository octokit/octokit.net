using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalOrgs: {0} DisabledOrgs: {1} TotalTeams: {2} TotalTeamMembers: {3}", TotalOrgs, DisabledOrgs, TotalTeams, TotalTeamMembers);
            }
        }
    }
}