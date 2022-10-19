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
        public string Name { get; private set; }

        /// <summary>
        /// The SHA of the reference.
        /// </summary>
        public string Sha { get; private set; }

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
        /// The relevant commit.
        /// </summary>
        public GitHubCommit Commit { get; private set; }

        /// <summary>
        /// The organization associated with the event.
        /// </summary>
        public Organization Organization { get; private set; }

        /// <summary>
        /// The branches involved.
        /// </summary>
        public IReadOnlyList<Branch> Branches { get; private set; }
    }
}
