using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryProjectColumn
    {
        public RepositoryProjectColumn() { }

        public RepositoryProjectColumn(int id, string name, string projectUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            ProjectUrl = projectUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The Id for this column.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The name for this column.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The URL for this columns project.
        /// </summary>
        public string ProjectUrl { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Number: {1}", Name, Number);
            }
        }
    }
}
