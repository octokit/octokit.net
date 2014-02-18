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
        public Author Author { get; set; }

        public int Total { get; set; }

        public IEnumerable<WeeklyHash> Weeks { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "Author: Id: {0} Login: {1}", Author.Id, Author.Login);
            }
        }
    }
}