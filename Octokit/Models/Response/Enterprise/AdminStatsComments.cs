namespace Octokit
{
    public class AdminStatsComments
    {
        public AdminStatsComments(int totalCommitComments, int totalGistComments, int totalIssueComments, int totalPullRequestComments)
        {
            TotalCommitComments = totalCommitComments;
            TotalGistComments = totalGistComments;
            TotalIssueComments = totalIssueComments;
            TotalPullRequestComments = totalPullRequestComments;
        }

        public int TotalCommitComments
        {
            get;
            private set;
        }

        public int TotalGistComments
        {
            get;
            private set;
        }

        public int TotalIssueComments
        {
            get;
            private set;
        }

        public int TotalPullRequestComments
        {
            get;
            private set;
        }
    }
}