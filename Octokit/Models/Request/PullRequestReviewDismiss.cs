using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to dismiss a pull request review
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewDismiss : RequestParameters
    {
        public PullRequestReviewDismiss()
        {
        }

        /// <summary>
        /// The message explaining why this review is being dismissed
        /// </summary>
        public string Message { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Message: {0} ", Message);
            }
        }
    }
}
