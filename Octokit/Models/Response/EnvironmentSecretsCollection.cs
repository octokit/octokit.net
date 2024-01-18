using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
	/// <summary>
	/// Represents response of secrets for a repository environment (environment).
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnvironmentSecretsCollection
	{
        public EnvironmentSecretsCollection()
        {
        }

        public EnvironmentSecretsCollection(int totalCount, IReadOnlyList<EnvironmentSecret> secrets)
        {
            TotalCount = totalCount;
            Secrets = secrets;
        }

        /// <summary>
        /// The total count of secrets for the environment
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The list of secrets for the environment
        /// </summary>
        public IReadOnlyList<EnvironmentSecret> Secrets { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "EnvironmentSecretsCollection: Count: {0}", TotalCount);
    }
}
