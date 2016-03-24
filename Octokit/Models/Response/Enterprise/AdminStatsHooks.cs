using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsHooks
    {
        public AdminStatsHooks() { }

        public AdminStatsHooks(int totalHooks, int activeHooks, int inactiveHooks)
        {
            TotalHooks = totalHooks;
            ActiveHooks = activeHooks;
            InactiveHooks = inactiveHooks;
        }

        public int TotalHooks
        {
            get;
            private set;
        }

        public int ActiveHooks
        {
            get;
            private set;
        }

        public int InactiveHooks
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalHooks: {0} ActiveHooks: {1} InactiveHooks: {2}", TotalHooks, ActiveHooks, InactiveHooks);
            }
        }
    }
}