using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A draft comment that is part of a Pull Request Review
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DraftPullRequestReviewComment : RequestParameters
    {
        /// <summary>
        /// Creates a draft comment
        /// </summary>
        /// <param name="body">The text of the comment</param>
        /// <param name="path">The relative path of the file to comment on</param>
        /// <param name="position">The line index in the diff to comment on</param>
        public DraftPullRequestReviewComment(string body, string path, int position)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, nameof(body));
            Ensure.ArgumentNotNullOrEmptyString(path, nameof(path));

            Body = body;
            Path = path;
            Position = position;
        }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The relative path of the file to comment on.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The line index in the diff to comment on.
        /// </summary>
        public int Position { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Path: {0}, Position: {1}", Path, Position); }
        }
    }
}
