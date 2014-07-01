
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
        public MergePullRequest(string message)
        {
            Ensure.ArgumentNotNull(message, "message");

            Message = message;
        }

        /// <summary>
        /// The message that will be used for the merge commit (optional)
        /// </summary>
        public string Message { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Message: {0}", Message);
            }
        }
    }
}
