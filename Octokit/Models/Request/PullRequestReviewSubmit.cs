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
        public StringEnum<PullRequestReviewEvent> Event { get; set; }
                
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Event: {0} ", Event);
            }
        }
    }

    public enum PullRequestReviewEvent
    {
        [Parameter(Value = "APPROVE")]
        Approve,
        
        [Parameter(Value = "REQUEST_CHANGES")]
        RequestChanges,

        [Parameter(Value = "COMMENT")]
        Comment
    }
}
