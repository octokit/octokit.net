using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PunchCard
    {
        public PunchCard(IEnumerable<IList<int>> punchCardData)
        {
            Ensure.ArgumentNotNull(punchCardData, "punchCardData");
            PunchPoints = new ReadOnlyCollection<PunchCardPoint>(
                punchCardData.Select(point => new PunchCardPoint(point)).ToList());
        }

        /// <summary>
        /// The raw punch card points
        /// </summary>
        public IReadOnlyCollection<PunchCardPoint> PunchPoints { get; private set; }

        /// <summary>
        /// Gets the number of commits made on the specified day of the week
        /// at the hour of the day, over the lifetime of this repository
        /// </summary>
        /// <param name="dayOfWeek">The day of the week to query</param>
        /// <param name="hourOfDay">The hour in 24 hour time. 0-23.</param>
        /// <returns>The total number of commits made.</returns>
        public int GetCommitCountFor(DayOfWeek dayOfWeek, int hourOfDay)
        {
            var punchPoint = PunchPoints.SingleOrDefault(point => point.DayOfWeek == dayOfWeek && point.HourOfTheDay == hourOfDay);
            return punchPoint == null ? 0 : punchPoint.CommitCount;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Punch Card Points: {0}", PunchPoints.Count);
            }
        }
    }
}