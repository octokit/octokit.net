using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a reply to a pull request review comment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentReplyCreate : RequestParameters
    {
        /// <summary>
        /// Creates a comment that is replying to another comment.
        /// </summary>
        /// <param name="body">The text of the comment</param>
        /// <param name="inReplyTo">The comment Id to reply to</param>
        public PullRequestReviewCommentReplyCreate(string body, int inReplyTo)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            Body = body;
            InReplyTo = inReplyTo;
        }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The comment Id to reply to.
        /// </summary>
        public int InReplyTo { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "InReplyTo: {0}, Body: {1}", InReplyTo, Body); }
        }
    }
}
