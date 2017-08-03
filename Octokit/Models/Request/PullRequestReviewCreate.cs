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
    public class PullRequestReviewCreate : RequestParameters
    {
        public PullRequestReviewCreate()
        {
            Comments = new List<PullRequestReviewCommentCreate>();
        }

        /// <summary>
        /// Which PullRequests to get. The default is <see cref="ItemStateFilter.Open"/>
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Filter pulls by head user and branch name in the format of "user:ref-name".
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Filter pulls by base branch name.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// What property to sort pull requests by.
        /// </summary>
        public List<PullRequestReviewCommentCreate> Comments { get; set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CommitId: {0} ", CommitId);
            }
        }
    }
}
