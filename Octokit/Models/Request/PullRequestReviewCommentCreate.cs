using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentCreate : RequestParameters
    {
        /// <summary>
        /// Creates a comment
        /// </summary>
        /// <param name="body">The text of the comment</param>
        /// <param name="commitId">The SHA of the commit to comment on</param>
        /// <param name="path">The relative path of the file to comment on</param>
        /// <param name="position">The line index in the diff to comment on</param>
        public PullRequestReviewCommentCreate(string body, string commitId, string path, int position)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");
            Ensure.ArgumentNotNullOrEmptyString(commitId, "commitId");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            Body = body;
            CommitId = commitId;
            Path = path;
            Position = position;
        }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The SHA of the commit to comment on.
        /// </summary>
        public string CommitId { get; private set; }

        /// <summary>
        /// The relative path of the file to comment on.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The line index in the diff to comment on.
        /// </summary>
        public int Position { get; private set; }
    }
}
