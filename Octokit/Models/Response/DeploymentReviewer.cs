using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Details of a deployment that is waiting for protection rules to pass.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeploymentReviewer
    {
        public DeploymentReviewer() { }

        public DeploymentReviewer(DeploymentReviewerType type, User reviewer)
        {
            Type = type;
            Reviewer = reviewer;
        }

        /// <summary>
        /// The type of the reviewer.
        /// </summary>
        public StringEnum<DeploymentReviewerType> Type { get; private set; }

        /// <summary>
        /// The user or team whose approval is required.
        /// </summary>
        public User Reviewer { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Type: {0}, Reviewer: {1}", Type, Reviewer);
            }
        }
    }
}
