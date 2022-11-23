using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents an environment for a deployment approval.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnvironmentApprovals
    {
        public EnvironmentApprovals() { }

        public EnvironmentApprovals(IReadOnlyList<EnvironmentApproval> environments, User user, EnvironmentApprovalState state, string comment)
        {
            Environments = environments;
            User = user;
            State = state;
            Comment = comment;
        }

        /// <summary>
        /// The list of environments that were approved or rejected.
        /// </summary>
        public IReadOnlyList<EnvironmentApproval> Environments { get; private set; }

        /// <summary>
        /// The user that approved or rejected the deployments.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Whether deployment to the environment(s) was approved or rejected.
        /// </summary>
        public StringEnum<EnvironmentApprovalState> State { get; private set; }

        /// <summary>
        /// The comment submitted with the deployment review.
        /// </summary>
        public string Comment { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "State: {0}, Comment: {1}", State, Comment);
            }
        }
    }
}
