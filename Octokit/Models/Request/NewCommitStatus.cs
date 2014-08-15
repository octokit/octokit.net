using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCommitStatus
    {
        /// <summary>
        /// The state of the commit.
        /// </summary>
        public CommitState State { get; set; }

        /// <summary>
        /// URL associated with this status. GitHub.com displays this URL as a link to allow users to easily see the
        /// ‘source’ of the Status. For example, if your Continuous Integration system is posting build status, 
        /// you would want to provide the deep link for the build output for this specific sha.
        /// </summary>
        public Uri TargetUrl { get; set; }

        /// <summary>
        /// Short description of the status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A string label to differentiate this status from the status of other systems.
        /// </summary>
        public string Context { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}, Context: {1}", Description, Context);
            }
        }
    }
}
