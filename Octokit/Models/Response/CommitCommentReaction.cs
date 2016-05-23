using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitCommentReaction
    {
        public CommitCommentReaction() { }

        public CommitCommentReaction(int id, int userId, string content)
        {
            Id = id;
            UserId = userId;
            Content = content;
        }

        /// <summary>
        /// The Id for this reaction.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The UserId.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// The reaction type for this commit comment.
        /// </summary>
        public String Content { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Reaction: {1}", Id, Content);
            }
        }
    }
}

