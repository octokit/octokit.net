using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectCard
    {
        public ProjectCard() { }

        public ProjectCard(string columnUrl, string contentUrl, int id, string nodeId, string note, User creator, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool archived)
        {
            ColumnUrl = columnUrl;
            ContentUrl = contentUrl;
            Id = id;
            NodeId = nodeId;
            Note = note;
            Creator = creator;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Archived = archived;
        }

        /// <summary>
        /// The URL for this cards column.
        /// </summary>
        public string ColumnUrl { get; private set; }

        /// <summary>
        /// The URL for this cards content.
        /// </summary>
        public string ContentUrl { get; private set; }

        /// <summary>
        /// The Id for this card.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The note for this card.
        /// </summary>
        public string Note { get; private set; }

        /// <summary>
        /// The user associated with this card.
        /// </summary>
        public User Creator { get; private set; }

        /// <summary>
        /// When this card was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// When this card was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// Whether this card is archived.
        /// </summary>
        public bool Archived { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Note: {0}, Id: {1}", Note, Id);
            }
        }
    }
}
