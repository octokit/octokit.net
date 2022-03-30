using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PendingDeploymentReview
    {
        public PendingDeploymentReview(IList<long> environmentIds, PendingDeploymentReviewState state, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyEnumerable(environmentIds, nameof(environmentIds));
            Ensure.ArgumentNotNull(comment, nameof(comment));

            EnvironmentIds = environmentIds;
            State = state;
            Comment = comment;
        }

        /// <summary>
        /// The list of environment Ids to approve or reject.
        /// </summary>
        public IList<long> EnvironmentIds { get; protected set; }

        /// <summary>
        /// Whether to approve or reject deployment to the specified environments.
        /// </summary>
        public StringEnum<PendingDeploymentReviewState> State { get; protected set; }

        /// <summary>
        /// A comment to accompany the deployment review.
        /// </summary>
        public string Comment { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "EnvironmentIds: {0}, State: {1}, Comment: {2}", EnvironmentIds, State, Comment); }
        }
    }
}
