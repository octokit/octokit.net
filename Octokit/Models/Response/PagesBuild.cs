using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Models.Response
{
    /// <summary>
    /// Metadata of a Github Pages build.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PagesBuild
    {
        public PagesBuild() { }

        public PagesBuild(string url, PagesBuildStatus status, User pusher, Commit commit, TimeSpan duration, DateTime createdAt, DateTime updatedAt)
        {
            Url = url;
            Status = status;
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
    }
}
