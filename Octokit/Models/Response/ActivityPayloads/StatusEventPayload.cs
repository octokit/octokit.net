using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StatusEventPayload : ActivityPayload
    {
        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The SHA of the reference.
        /// </summary>
        public string Sha { get; protected set; }

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
        public StringEnum<CommitState> State { get; protected set; }

        /// <summary>
        /// URL associated with this status. GitHub.com displays this URL as a link to allow users to easily see the
        /// ‘source’ of the Status.
        /// </summary>
        public string TargetUrl { get; protected set; }

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
        public long Id { get; protected set; }

        /// <summary>
        /// The relevant commit.
        /// </summary>
        public GitHubCommit Commit { get; protected set; }

        /// <summary>
        /// The organization associated with the event.
        /// </summary>
        public Organization Organization { get; protected set; }

        /// <summary>
        /// The branches involved.
        /// </summary>
        public IReadOnlyList<Branch> Branches { get; protected set; }

    }
}
