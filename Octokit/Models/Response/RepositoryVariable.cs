using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a repository variable.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryVariable
    {
        public RepositoryVariable()
        {
        }

        public RepositoryVariable(string name, string value, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
        {
            Name = name;
            Value = value;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The name of the repository variable
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The value of the repository variable
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// The date and time that the variable was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date and time the variable was last updated
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositoryVariable: Name: {0}", Name);
            }
        }
    }
}
