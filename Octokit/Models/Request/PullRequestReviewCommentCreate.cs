using Octokit.Internal;

namespace Octokit
{
    public class PullRequestReviewCommentCreate : RequestParameters
    {
        private readonly string _body;
        private readonly string _commitId;
        private readonly string _path;
        private readonly int _position;

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get { return _body; } }

        /// <summary>
        /// The SHA of the commit to comment on.
        /// </summary>
        public string CommitId { get { return _commitId; } }

        /// <summary>
        /// The relative path of the file to comment on.
        /// </summary>
        public string Path { get { return _path; } }

        /// <summary>
        /// The line index in the diff to comment on.
        /// </summary>
        public int Position { get { return _position; } }

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

            _body = body;
            _commitId = commitId;
            _path = path;
            _position = position;
        }
    }
}
