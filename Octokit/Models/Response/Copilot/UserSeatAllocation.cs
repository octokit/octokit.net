using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Holds information about user names to be added or removed from a Copilot-enabled organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserSeatAllocation
    {
        /// <summary>
        /// One or more usernames to be added or removed from a Copilot-enabled organization.
        /// </summary>
        public string[] SelectedUsernames { get; set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SelectedUsernames: {0}", string.Join(",", SelectedUsernames));
            }
        }
    }
}