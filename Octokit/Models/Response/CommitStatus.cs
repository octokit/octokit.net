using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitStatus
    {
        public CommitStatus() { }

        public CommitStatus(DateTimeOffset createdAt, DateTimeOffset updatedAt, CommitState state, Uri targetUrl, string description, string context, int id, Uri url, User creator)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            State = state;
            TargetUrl = targetUrl;
            Description = description;
            Context = context;
            Id = id;
            Url = url;
            Creator = creator;
        }

        /// <summary>
        /// The date the commit status was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date the commit status was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// The state of the commit
        /// </summary>
        public CommitState State { get; protected set; }

        /// <summary>
        /// URL associated with this status. GitHub.com displays this URL as a link to allow users to easily see the
        /// ‘source’ of the Status.
        /// </summary>
        public Uri TargetUrl { get; protected set; }

        /// <summary>
        /// Short description of the status.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// A string label to differentiate this status from the status of other systems.
        /// </summary>
        public string Context { get; protected set; }

        /// <summary>
        /// The unique identifier of the status.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL of the status.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The user that created the status.
        /// </summary>
        public User Creator { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CreatedAt: {0} State: {1}", CreatedAt, State);
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
