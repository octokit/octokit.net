using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

        public int Count { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; private set; }

        public IReadOnlyList<RepositoryTrafficClone> Clones { get; private set; }

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
        public RepositoryTrafficClone(DateTimeOffset timestamp, int count, int uniques)
        {
            Timestamp = timestamp;
            Count = count;
            Uniques = uniques;
        }

        public DateTimeOffset Timestamp { get; private set; }

        public int Count { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    public enum TrafficDayOrWeek
    {
        [Parameter(Value = "day")]
        Day,

        [Parameter(Value = "week")]
        Week
    }
}
