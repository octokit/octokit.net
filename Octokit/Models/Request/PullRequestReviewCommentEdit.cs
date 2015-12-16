using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to edit a pull request review comment
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentEdit : RequestParameters
    {
        /// <summary>
        /// Creates an edit to a comment
        /// </summary>
        /// <param name="body">The new text of the comment</param>
        public PullRequestReviewCommentEdit(string body)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            Body = body;
        }

        /// <summary>
        /// The new text of the comment.
        /// </summary>
        public string Body { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Body: {0}", Body); }
        }
    }
}
