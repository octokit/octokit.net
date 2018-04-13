using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Describes the current download state of a pre-receive environment image.
    /// </summary>
    public class PreReceiveEnvironmentDownload
    {
        /// <summary>
        /// URL to the download status for a pre-receive environment.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The state of the most recent download.
        /// </summary>
        public PreReceiveEnvironmentDownloadState State { get; set; }

        /// <summary>
        /// On failure, this will have any error messages produced.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The time when the most recent download started.
        /// </summary>
        [Parameter(Key = "downloaded_at")]
        public DateTimeOffset? DownloadedAt { get; set; }
    }
}
