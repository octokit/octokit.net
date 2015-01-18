using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentEdit : RequestParameters
    {
        /// <summary>
        /// Creates an edit to a comment
        /// </summary>
        /// <param name="body">The text of the comment</param>
        public PullRequestReviewCommentEdit(string body)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            Body = body;
        }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Body: {0}", Body); }
        }
    }
}
