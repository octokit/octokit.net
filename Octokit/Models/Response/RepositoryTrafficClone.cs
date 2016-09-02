using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficClone
    {
        public RepositoryTrafficClone() { }

        public RepositoryTrafficClone(int count, int uniques, IReadOnlyList<Clone> clones)
        {
            Count = count;
            Uniques = uniques;
            Clones = clones;
        }

        public int Count { get; protected set; }

        public int Uniques { get; protected set; }

        public IReadOnlyList<Clone> Clones { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Clone
    {
        public Clone() { }

        public Clone(DateTimeOffset timeStamp, int count, int uniques)
        {
            TimeStamp = timeStamp;
            Count = count;
            Uniques = uniques;
        }

        public DateTimeOffset TimeStamp { get; protected set; }

        public int Count { get; protected set; }

        public int Uniques { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }
}
