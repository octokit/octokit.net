using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependencySnapshotSubmission
    {
        public DependencySnapshotSubmission() { }

        public DependencySnapshotSubmission(long id, DateTimeOffset createdAt, DependencySnapshotSubmissionResult result, string message)
        {
            Id = id;
            CreatedAt = createdAt;
            Result = result;
            Message = message;
        }

        /// <summary>
        /// ID of the created snapshot.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The time at which the snapshot was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The outcome of the dependency snapshot creation.
        /// </summary>
        public StringEnum<DependencySnapshotSubmissionResult> Result { get; private set; }

        /// <summary>
        /// A message providing further details about the result, such as why the dependencies were not updated.
        /// </summary>
        public string Message { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} Created at: {1}", Id, CreatedAt);
            }
        }
    }

    public enum DependencySnapshotSubmissionResult
    {
        /// <summary>
        /// The snapshot was successfully created and the repository's dependencies were updated.
        /// </summary>
        [Parameter(Value = "Success")]
        Success,

        /// <summary>
        /// The snapshot was successfully created, but the repository's dependencies were not updated.
        /// </summary>
        [Parameter(Value = "Accepted")]
        Accepted,

        /// <summary>
        /// The snapshot was malformed.
        /// </summary>
        [Parameter(Value = "Invalid")]
        Invalid
    }
}
