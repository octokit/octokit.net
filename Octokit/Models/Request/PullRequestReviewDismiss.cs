using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for lists of pull requests.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewDismiss : RequestParameters
    {
        public PullRequestReviewDismiss()
        {
        }

        /// <summary>
        /// Which PullRequests to get. The default is <see cref="ItemStateFilter.Open"/>
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
