using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Edits the title and body text of a discussion post. Only the parameters you provide are updated.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In order to create a team discussions, the OAuth access tokens require the write:discussion scope.
    /// </para>
    /// <para>API: https://developer.github.com/v3/teams/discussions/#create-a-discussion </para>
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeamDiscussion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTeamDiscussion"/> class.
        /// </summary>
        /// <param name="title">The discussion post's title.</param>
        /// <param name="body">The discussion post's body text.</param>
        public UpdateTeamDiscussion(string title, string body)
        {
            Title = title;
            Body = body;
        }

        /// <summary>
        /// The discussion post's title (optional).
        /// </summary>
        public string Title { get; private set; }

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