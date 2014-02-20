
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new deployment status to create.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDeployment
    {
        /// <summary>
        /// The ref to deploy. This can be a branch, tag, or sha.
        /// </summary>
        public string Ref { get; set; }

        /// <summary>
        /// Optional parameter to bypass any ahead/behind checks or commit status checks.
        /// </summary>
        public bool? Force { get; set; }

        /// <summary>
        /// JSON payload with extra information about the deployment.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Merges the default branch into the requested deployment branch if true;
        /// Does nothing if false.
        /// </summary>
        public bool? AutoMerge { get; set; }

        /// <summary>
        /// A short description of the deployment.
        /// </summary>
        public string Description { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }
}
