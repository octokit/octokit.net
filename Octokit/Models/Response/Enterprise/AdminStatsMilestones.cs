namespace Octokit
{
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
    }
}