using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to fork a repository.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/repos/forks/#create-a-fork
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRepositoryFork
    {
        /// <summary>
        /// Gets or sets the organization name to fork into (Optional). If not specified, creates a fork for the
        /// authenticated user.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository Hook: Organization: {0}", Organization);
            }
        }
    }
}