using System.Collections.Generic;

namespace Octokit
{
    public class CommitActivity
    {
        public CommitActivity(IEnumerable<WeeklyCommitActivity> activity)
        {
            Activity = activity;
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        public IEnumerable<WeeklyCommitActivity> Activity { get; private set; }
    }
}