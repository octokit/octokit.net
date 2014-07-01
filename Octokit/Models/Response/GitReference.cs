using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitReference
    {
        /// <summary>
        /// The URL associated with this reference.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The reference label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The reference identifier.
        /// </summary>
        public string Ref { get; set; }

        /// <summary>
        /// The sha value of the reference.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The user associated with this reference.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The repository associated with this reference.
        /// </summary>
        [Parameter(Key = "repo")]
        public Repository Repository { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}