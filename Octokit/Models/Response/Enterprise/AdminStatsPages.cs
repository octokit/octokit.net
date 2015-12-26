namespace Octokit
{
    public class AdminStatsPages
    {
        public AdminStatsPages() { }

        public AdminStatsPages(int totalPages)
        {
            TotalPages = totalPages;
        }

        public int TotalPages
        {
            get;
            private set;
        }
    }
}