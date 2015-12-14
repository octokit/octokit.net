using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Specifies the values used to update a <see cref="Branch"/>.
    /// Note: this is a PREVIEW api: https://developer.github.com/changes/2015-11-11-protected-branches-api/
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
