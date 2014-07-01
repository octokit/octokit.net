using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitStatus
    {
        /// <summary>
        /// The date the commit status was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date the commit status was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The state of the commit
        /// </summary>
        public CommitState State { get; set; }

        /// <summary>
        /// URL associated with this status. GitHub.com displays this URL as a link to allow users to easily see the
        /// ‘source’ of the Status.
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

        /// <summary>
        /// The unique identifier of the status.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The URL of the status.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The user that created the status.
        /// </summary>
        public User Creator { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "CreatedAt: {0} State: {1}", CreatedAt, State);
            }
        }
    }

    /// <summary>
    /// Represents the state of a commit.
    /// </summary>
    public enum CommitState
    {
        /// <summary>
        /// The commit state is still being determined. A build server might set this when it starts a build.
        /// </summary>
        Pending,

        /// <summary>
        /// The build was successful for the commit.
        /// </summary>
        Success,

        /// <summary>
        /// There was some error with the build.
        /// </summary>
        Error,

        /// <summary>
        /// The build completed and reports a failure.
        /// </summary>
        Failure
    }
}
