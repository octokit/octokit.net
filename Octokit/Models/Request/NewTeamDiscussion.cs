using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a team discussion.
    /// </summary>
    /// <remarks>
    /// OAuth access tokens require the write:discussion scope.
    /// See the <a href="https://developer.github.com/v3/teams/discussions/#create-a-discussion">Create a discussion</a> for more information.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTeamDiscussion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTeamDiscussion"/> class.
        /// </summary>
        /// <param name="title">The discussion post's title.</param>
        /// <param name="body">The discussion post's body text.</param>
        public NewTeamDiscussion(string title, string body)
        {
            Title = title;
            Body = body;
        }

        /// <summary>
        /// The discussion post's title (required).
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The discussion post's body text (required).
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Private posts are only visible to team members, organization owners, and team maintainers.
        /// Public posts are visible to all members of the organization. Set to true to create a private post.
        /// Default: false.
        /// </summary>
        public bool? Private { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Title: {0} Body: {1}", Title, Body);
            }
        }
    }
}