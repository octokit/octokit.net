using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitStatus
    {
        public CommitStatus() { }

        public CommitStatus(DateTimeOffset createdAt, DateTimeOffset updatedAt, CommitState state, string targetUrl, string description, string context, long id, string nodeId, string url, User creator)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            State = state;
            TargetUrl = targetUrl;
            Description = description;
            Context = context;
            Id = id;
            NodeId = nodeId;
            Url = url;
            Creator = creator;
        }

        /// <summary>
        /// The date the commit status was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The date the commit status was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// The state of the commit
        /// </summary>
        public StringEnum<CommitState> State { get; private set; }

        /// <summary>
        /// URL associated with this status. GitHub.com displays this URL as a link to allow users to easily see the
        /// ‘source’ of the Status.
        /// </summary>
        public string TargetUrl { get; private set; }

        /// <summary>
        /// Short description of the status.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// A string label to differentiate this status from the status of other systems.
        /// </summary>
        public string Context { get; private set; }

        /// <summary>
        /// The unique identifier of the status.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The URL of the status.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The user that created the status.
        /// </summary>
        public User Creator { get; private set; }

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
        [Parameter(Value = "pending")]
        Pending,

        /// <summary>
        /// The build was successful for the commit.
        /// </summary>
        [Parameter(Value = "success")]
        Success,

        /// <summary>
        /// There was some error with the build.
        /// </summary>
        [Parameter(Value = "error")]
        Error,

        /// <summary>
        /// The build completed and reports a failure.
        /// </summary>
        [Parameter(Value = "failure")]
        Failure
    }
}
