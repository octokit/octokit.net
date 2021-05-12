using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Edits the title and body text of a discussion post. Only the parameters you provide are updated.
    /// </summary>
    /// <remarks>
    /// OAuth access tokens require the write:discussion scope.
    /// See the <a href="https://developer.github.com/v3/teams/discussions/#edit-a-discussion">Edit a discussion</a> for more information.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeamDiscussion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeamDiscussion"/> class.
        /// </summary>
        public UpdateTeamDiscussion()
        {
        }

        /// <summary>
        /// The discussion post's title (optional).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The discussion post's body text (optional).
        /// </summary>
        public string Body { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: {0} Body: {1}", Title, Body);
            }
        }
    }
}