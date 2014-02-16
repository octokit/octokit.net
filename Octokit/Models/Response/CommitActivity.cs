using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Weeks of activity: {0}",Activity.Count());
            }
        }
    }
}