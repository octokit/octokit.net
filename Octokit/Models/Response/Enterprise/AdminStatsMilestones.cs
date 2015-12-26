using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsMilestones
    {
        public AdminStatsMilestones() { }

        public AdminStatsMilestones(int totalMilestones, int openMilestones, int closedMilestones)
        {
            TotalMilestones = totalMilestones;
            OpenMilestones = openMilestones;
            ClosedMilestones = closedMilestones;
        }

        public int TotalMilestones
        {
            get;
            private set;
        }

        public int OpenMilestones
        {
            get;
            private set;
        }

        public int ClosedMilestones
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalMilestones: {0} OpenMilestones: {1} ClosedMilestones: {2}", TotalMilestones, OpenMilestones, ClosedMilestones);
            }
        }
    }
}