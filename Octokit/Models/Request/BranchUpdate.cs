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
    [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please use BranchProtectionSettingsUpdate instead.")]
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
                return string.Format(CultureInfo.InvariantCulture, "Protection: {0}", Protection.DebuggerDisplay);
            }
        }
    }
}
