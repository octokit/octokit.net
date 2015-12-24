namespace Octokit
{
    public class AdminStatsGists
    {
        public AdminStatsGists(int totalGists, int privateGists, int publicGists)
        {
            TotalGists = totalGists;
            PrivateGists = privateGists;
            PublicGists = publicGists;
        }

        public int TotalGists
        {
            get;
            private set;
        }

        public int PrivateGists
        {
            get;
            private set;
        }

        public int PublicGists
        {
            get;
            private set;
        }
    }
}