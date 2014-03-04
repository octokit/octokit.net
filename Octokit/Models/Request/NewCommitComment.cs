using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new commit comment to create via the <see cref="IRepositoryCommentsClient.Create(string,string,string,NewCommitComment)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCommitComment
    {
        public NewCommitComment(string body)
        {
            Ensure.ArgumentNotNull(body, "body");

            Body = body;
        }

        /// <summary>
        /// The contents of the comment (required)
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Relative path of the file to comment on
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Line index in the diff to comment on
        /// </summary>
        public int? Position { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Path: {0}, Body: {1}", Path, Body);
            }
        }
    }
}
