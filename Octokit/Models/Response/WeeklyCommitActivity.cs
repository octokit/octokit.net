using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Octokit.Helpers;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
        public long Week { get; set; }

        public DateTimeOffset WeekTimestamp
        {
            get { return Week.FromUnixTime(); }
        }

        /// <summary>
        /// Get the number of commits made on any <see cref="DayOfWeek"/>
        /// </summary>
        /// <param name="dayOfWeek">The day of the week</param>
        /// <returns>The number of commits made</returns>
        public int GetCommitCountOn(DayOfWeek dayOfWeek)
        {
            return Days.ElementAt((int)dayOfWeek);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Week: {0} Total Commits: {1}", WeekTimestamp, Total);
            }
        }
    }
}