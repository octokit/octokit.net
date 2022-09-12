using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WeeklyCommitActivity
    {
        public WeeklyCommitActivity() { }

        public WeeklyCommitActivity(IEnumerable<int> days, int total, long week)
        {
            Ensure.ArgumentNotNull(days, nameof(days));

            Days = new ReadOnlyCollection<int>(days.ToList());
            Total = total;
            Week = week;
        }

        /// <summary>
        /// The days array is a group of commits per day, starting on Sunday.
        /// </summary>
        public IReadOnlyList<int> Days { get; private set; }

        /// <summary>
        /// Totally number of commits made this week.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// The week of commits
        /// </summary>
        public long Week { get; private set; }

        public DateTimeOffset WeekTimestamp => DateTimeOffset.FromUnixTimeSeconds(Week);

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
                return string.Format(CultureInfo.InvariantCulture,
                    "Week: {0} Total Commits: {1}", WeekTimestamp, Total);
            }
        }
    }
}
