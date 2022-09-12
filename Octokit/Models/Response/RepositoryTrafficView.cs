using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficViewSummary
    {
        public RepositoryTrafficViewSummary() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficViewSummary(int count, int uniques, IReadOnlyList<RepositoryTrafficView> views)
        {
            Count = count;
            Uniques = uniques;
            Views = views;
        }

        public int Count { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public int Uniques { get; private set; }

        public IReadOnlyList<RepositoryTrafficView> Views { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficView
    {
        public RepositoryTrafficView() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "It's a property from the api.")]
        public RepositoryTrafficView(DateTimeOffset timestamp, int count, int uniques)
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
            get { return string.Format(CultureInfo.InvariantCulture, "Timestamp: {0} Number: {1} Uniques: {2}", Timestamp, Count, Uniques); }
        }
    }
}
