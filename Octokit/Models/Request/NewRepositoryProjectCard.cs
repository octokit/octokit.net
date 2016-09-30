using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryProjectCard
    {
        public NewRepositoryProjectCard(string note)
        {
            Note = note;
        }

        public NewRepositoryProjectCard(int contentId)
        {
            ContentId = contentId;
        }

        /// <summary>
        /// The note of the card.
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        /// The id of the Issue or Pull Request to associate with this card.
        /// </summary>
        [Parameter(Key = "content_id")]
        public int ContentId { get; protected set; }

        /// <summary>
        /// The type of content to associate with this card.
        /// </summary>
        [Parameter(Key = "content_type")]
        public ProjectCardContentType ContentType { get; set; }

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
        Issue,
        PullRequest
    }
}
