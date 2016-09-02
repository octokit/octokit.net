using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTrafficView
    {
        public RepositoryTrafficView() { }

        public RepositoryTrafficView(int count, int uniques, IReadOnlyList<View> views)
        {
            Count = count;
            Uniques = uniques;
            Views = views;
        }

        public int Count { get; protected set; }

        public int Uniques { get; protected set; }

        public IReadOnlyList<View> Views { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} Uniques: {1}", Count, Uniques); }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class View
    {
        public View() { }

        public View(DateTimeOffset timeStamp, int count, int uniques)
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
