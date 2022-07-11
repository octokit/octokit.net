using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A Reference to a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryReference
    {
        /// <summary>
        /// The full name for the repository.
        /// </summary>
        public string FullName { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "FullName: {0}", FullName); }
        }
    }
}