using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Details of a deployment that is waiting for protection rules to pass.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PendingDeployment
    {
        public PendingDeployment() { }

        public PendingDeployment(PendingDeploymentEnvironment environment, long waitTimer, DateTimeOffset? waitTimerStartedAt, bool currentUserCanApprove, IReadOnlyList<DeploymentReviewer> reviewers)
        {
            Environment = environment;
            WaitTimer = waitTimer;
            WaitTimerStartedAt = waitTimerStartedAt;
            CurrentUserCanApprove = currentUserCanApprove;
            Reviewers = reviewers;
        }

        /// <summary>
        /// The environment pending deployment.
        /// </summary>
        public PendingDeploymentEnvironment Environment { get; private set; }

        /// <summary>
        /// The set duration of the wait timer.
        /// </summary>
        public long WaitTimer { get; private set; }

        /// <summary>
        /// The time that the wait timer began.
        /// </summary>
        public DateTimeOffset? WaitTimerStartedAt { get; private set; }

        /// <summary>
        /// Whether the currently authenticated user can approve the deployment.
        /// </summary>
        public bool CurrentUserCanApprove { get; private set; }

        /// <summary>
        /// The people or teams that may approve jobs that reference the environment.
        /// </summary>
        public IReadOnlyList<DeploymentReviewer> Reviewers { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CurrentUserCanApprove: {0}", CurrentUserCanApprove);
            }
        }
    }
}
