using System.Collections.Generic;

namespace Octokit
{
    public class WeeklyCommitActivity
    {
        //The days array is a group of commits per day, starting on Sunday.
        public IEnumerable<int> Days { get; set; } 

        public int Total { get; set; }

        public int Week { get; set; }
    }
}