using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a contributor on GitHub.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Contributor
    {
        public Contributor() { }

        public Contributor(Author author, int total, IReadOnlyList<WeeklyHash> weeks)
        {
            Author = author;
            Total = total;
            Weeks = weeks;
        }

        public Author Author { get; protected set; }

        public int Total { get; protected set; }

        public IReadOnlyList<WeeklyHash> Weeks { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Author: Id: {0} Login: {1}", Author.Id, Author.Login);
            }
        }
    }
}