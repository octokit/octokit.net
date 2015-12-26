namespace Octokit
{
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
    }
}