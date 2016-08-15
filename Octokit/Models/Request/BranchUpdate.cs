using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Specifies the values used to update a <see cref="Branch"/>.
    /// </summary>
    /// <remarks>
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
    public class BranchUpdate
    {
        /// <summary>
        /// The <see cref="BranchProtection"/> details
        /// </summary>
        public BranchProtection Protection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Protection: {0}", Protection.DebuggerDisplay);
            }
        }
    }
}
