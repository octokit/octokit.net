
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to merge a pull request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MergePullRequest
    {
        /// <summary>
        /// The message that will be used for the merge commit (optional)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The SHA that pull request head must match to allow merge (optional)
        /// </summary>
        public string Sha { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: '{0}', Sha: '{1}'", Message, Sha);
            }
        }
    }
}
