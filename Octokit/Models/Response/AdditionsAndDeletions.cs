using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Helpers;

namespace Octokit
{
    /// <summary>
    /// Represents lines added and deleted at a given point in time
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdditionsAndDeletions
    {
        public AdditionsAndDeletions() { }

        public AdditionsAndDeletions(DateTimeOffset timestamp, int additions, int deletions)
        {
            Timestamp = timestamp;
            Additions = additions;
            Deletions = deletions;
        }

        /// <summary>
        /// Construct an instance of AdditionsAndDeletions
        /// </summary>
        /// <param name="additionsAndDeletions"></param>
        /// <exception cref="ArgumentException">If the list of data points is not 3 elements</exception>
        public AdditionsAndDeletions(IList<long> additionsAndDeletions)
        {
            Ensure.ArgumentNotNull(additionsAndDeletions, "additionsAndDeletions");
            if (additionsAndDeletions.Count != 3)
            {
                throw new ArgumentException("Addition and deletion aggregate must only contain three data points.");
            }
            Timestamp = additionsAndDeletions[0].FromUnixTime();
            Additions = Convert.ToInt32(additionsAndDeletions[1]);
            Deletions = Convert.ToInt32(additionsAndDeletions[2]);
        }

        /// <summary>
        /// Date of the recorded activity
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// Lines added for the given day
        /// </summary>
        public int Additions { get; private set; }

        /// <summary>
        /// Lines deleted for the given day
        /// </summary>
        public int Deletions { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "{0}: Additions: {1} Deletions: {2}", Timestamp.ToString("d", CultureInfo.InvariantCulture), Additions, Deletions);
            }
        }
    }
}