using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PunchCardPoint
    {
        public PunchCardPoint() { }

        public PunchCardPoint(IList<int> punchPoint)
        {
            Ensure.ArgumentNotNull(punchPoint, nameof(punchPoint));
            if (punchPoint.Count != 3)
            {
                throw new ArgumentException("Daily punch card must only contain three data points.");
            }
            DayOfWeek = (DayOfWeek)punchPoint[0];
            HourOfTheDay = punchPoint[1];
            CommitCount = punchPoint[2];
        }

        public PunchCardPoint(DayOfWeek dayOfWeek, int hourOfTheDay, int commitCount)
        {
            DayOfWeek = dayOfWeek;
            HourOfTheDay = hourOfTheDay;
            CommitCount = commitCount;
        }

        public StringEnum<DayOfWeek> DayOfWeek { get; private set; }
        public int HourOfTheDay { get; private set; }
        public int CommitCount { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Day: {0} Hour: {1} Commit Count:{2}", DayOfWeek, HourOfTheDay, CommitCount);
            }
        }
    }
}