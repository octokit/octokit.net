﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ProjectCard
    {
        public ProjectCard() { }

        public ProjectCard(string columnUrl, string contentUrl, int id, string nodeId, string note, User creator, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            ColumnUrl = columnUrl;
            ContentUrl = contentUrl;
            Id = id;
            NodeId = nodeId;
            Note = note;
            Creator = creator;
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
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The note for this card.
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        /// The user associated with this card.
        /// </summary>
        public User Creator { get; protected set; }

        /// <summary>
        /// When this card was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// When this card was last updated.
        /// </summary>
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
