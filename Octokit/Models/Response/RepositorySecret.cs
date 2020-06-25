using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a repository secret.
    /// Does not contain the secret value
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositorySecret
    {
        public RepositorySecret()
        {

        }

        public RepositorySecret(string name, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The name of the repository secret
        /// </summary>
        [Parameter(Key = "name")]
        public string Name { get; protected set; }

        /// <summary>
        /// The date and time that the secret was created
        /// </summary>
        [Parameter(Key = "created_at")]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// The date and time the secret was last updated
        /// </summary>
        [Parameter(Key = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositorySecret: Name: {0}", Name);
            }
        }
    }
}
