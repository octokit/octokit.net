using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryProjectCard
    {
        public RepositoryProjectCard() { }

        public RepositoryProjectCard(string columnUrl, string contentUrl, int id, string note, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            ColumnUrl = columnUrl;
            ContentUrl = contentUrl;
            Id = id;
            Note = note;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The URL for this cards column.
        /// </summary>
        public string ColumnUrl { get; protected set; }

        /// <summary>
        /// The URL for this cards content.
        /// </summary>
        public string ContentUrl { get; protected set; }

        /// <summary>
        /// The Id for this card.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The note for this card.
        /// </summary>
        public string Note { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Note: {0}, Id: {1}", Note, Id);
            }
        }
    }
}
