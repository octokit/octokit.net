namespace Octokit
{
    public class AdminStatsIssues
    {
        public AdminStatsIssues(int totalIssues, int openIssues, int closedIssues)
        {
            TotalIssues = totalIssues;
            OpenIssues = openIssues;
            ClosedIssues = closedIssues;
        }

        public int TotalIssues
        {
            get;
            private set;
        }

        public int OpenIssues
        {
            get;
            private set;
        }

        public int ClosedIssues
        {
            get;
            private set;
        }
    }
}