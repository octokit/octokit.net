using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Metadata of a Github Pages build.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PagesBuild
    {
        public PagesBuild() { }

        public PagesBuild(string url, PagesBuildStatus status, ApiError error, User pusher, Commit commit, TimeSpan duration, DateTime createdAt, DateTime updatedAt)
        {
            Url = url;
            Status = status;
            Error = error;
            Pusher = pusher;
            Commit = commit;
            Duration = duration;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The pages's API URL.
        /// </summary>
        public string Url { get; protected set; }
        /// <summary>
        /// The status of the build.
        /// </summary>
        public PagesBuildStatus Status { get; protected set; }
        /// <summary>
        /// Error details - if there was one.
        /// </summary>
        public ApiError Error { get; protected set; }
        /// <summary>
        /// The user whose commit intiated the build.
        /// </summary>
        public User Pusher { get; protected set; }
        /// <summary>
        /// Commit SHA.
        /// </summary>
        public Commit Commit { get; protected set; }
        /// <summary>
        /// Duration of the build
        /// </summary>
        public TimeSpan Duration { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Pusher: {0}, Status: {1}, Duration: {2}", Pusher.Name, Status.ToString(), Duration.TotalMilliseconds);
            }
        }
    }
}
