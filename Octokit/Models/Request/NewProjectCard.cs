using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewProjectCard
    {
        public NewProjectCard(string note)
        {
            Note = note;
        }

        public NewProjectCard(long contentId, ProjectCardContentType contentType)
        {
            ContentId = contentId;
            ContentType = contentType;
        }

        /// <summary>
        /// The note of the card.
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        /// The id of the Issue or Pull Request to associate with this card.
        /// </summary>
        [Parameter(Key = "content_id")]
        public long? ContentId { get; protected set; }

        /// <summary>
        /// The type of content to associate with this card.
        /// </summary>
        [Parameter(Key = "content_type")]
        public ProjectCardContentType? ContentType { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Note: {0}, Id: {1}", Note, ContentId);
            }
        }
    }

    public enum ProjectCardContentType
    {
        [Parameter(Value = nameof(Issue))]
        Issue,
        [Parameter(Value = nameof(PullRequest))]
        PullRequest
    }
}
