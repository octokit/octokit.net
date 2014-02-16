using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    public class WeeklyCommitActivity
    {
        /// <summary>
        /// The days array is a group of commits per day, starting on Sunday.
        /// </summary>
        public IEnumerable<int> Days { get; set; } 

        /// <summary>
        /// Totally number of commits made this week.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The week of commits
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// Get the number of commits made on any <see cref="DayOfWeek"/>
        /// </summary>
        /// <param name="dayOfWeek">The day of the week</param>
        /// <returns>The number of commits made</returns>
        public int GetCommitCountOn(DayOfWeek dayOfWeek)
        {
            return Days.ElementAt((int)dayOfWeek);
        }
    }
}