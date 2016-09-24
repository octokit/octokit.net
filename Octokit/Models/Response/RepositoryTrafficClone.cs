using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Helpers;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficCloneSummary
    {
        public RepositoryTrafficCloneSummary() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficCloneSummary(int count, int uniques, IReadOnlyList<RepositoryTrafficClone> clones)
        {
            Count = count;
            Uniques = uniques;
            Clones = clones;
        }

        public int Count { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; protected set; }

        public IReadOnlyList<RepositoryTrafficClone> Clones { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficClone
    {
        public RepositoryTrafficClone() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficClone(long timestamp, int count, int uniques)
        {
            TimestampAsUtcEpochSeconds = timestamp;
            Count = count;
            Uniques = uniques;
        }

        [Parameter(Key = "ignoreThisField")]
        public DateTimeOffset Timestamp
        {
            get { return TimestampAsUtcEpochSeconds.FromUnixTime(); }
        }

        [Parameter(Key = "timestamp")]
        public long TimestampAsUtcEpochSeconds { get; protected set; }

        public int Count { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    public enum TrafficDayOrWeek
    {
        Day,
        Week
    }
}
