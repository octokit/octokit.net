using System;

namespace Octokit
{
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
    }
}
