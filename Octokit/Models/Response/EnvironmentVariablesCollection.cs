using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents response of variables for an environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnvironmentVariablesCollection
    {
        public EnvironmentVariablesCollection()
        {
        }

        public EnvironmentVariablesCollection(int totalCount, IReadOnlyList<EnvironmentVariable> variables)
        {
            TotalCount = totalCount;
            Variables = variables;
        }

        /// <summary>
        /// The total count of variables for the environment
        /// </summary>
        public int TotalCount { get; private set; }

		/// <summary>
		/// The list of variables for the environment
		/// </summary>
		public IReadOnlyList<EnvironmentVariable> Variables { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "EnvironmentVariablesCollection: Count: {0}", TotalCount);
    }
}
