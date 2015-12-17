using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Encapsulates the parameters for a request to retrieve commits.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitRequest : RequestParameters
    {
        /// <summary>
        /// SHA or branch to start listing commits from.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Only commits containing this file path will be returned.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// GitHub login or email address by which to filter by commit author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Only commits after this date will be returned.
        /// </summary>
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Only commits before this date will be returned.
        /// </summary>
        public DateTimeOffset? Until { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} ", Sha);
            }
        }
    }
}
