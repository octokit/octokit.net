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
    public class PullRequestReviewSubmit : RequestParameters
    {
        public PullRequestReviewSubmit()
        {
        }
        
        /// <summary>
        /// Filter pulls by head user and branch name in the format of "user:ref-name".
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Filter pulls by base branch name.
        /// </summary>
        public PullRequestReviewSubmitEvents Event { get; set; }
                
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Event: {0} ", Event);
            }
        }
    }

    public enum PullRequestReviewSubmitEvents
    {
        [Parameter(Value = "APPROVE")]
        APPROVE,
        [Parameter(Value = "REQUEST_CHANGES")]
        REQUEST_CHANGES,
        [Parameter(Value = "COMMENT")]
        COMMENT
    }
}
