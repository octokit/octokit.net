using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
	/// <summary>
	/// Represents a repository environment (environment) secret.
	/// Does not contain the secret value
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnvironmentSecret
	{
        public EnvironmentSecret()
        {
        }

        public EnvironmentSecret(string name, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
        {
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The name of the environment secret
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The date and time that the secret was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date and time the secret was last updated
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
					"EnvironmentSecret: Name: {0}", Name);
            }
        }
    }
}
