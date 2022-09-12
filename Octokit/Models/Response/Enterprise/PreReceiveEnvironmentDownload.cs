using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes the current download state of a pre-receive environment image.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PreReceiveEnvironmentDownload
    {
        public PreReceiveEnvironmentDownload()
        { }

        public PreReceiveEnvironmentDownload(string url, PreReceiveEnvironmentDownloadState state, string message, DateTimeOffset? downloadedAt)
        {
            Url = url;
            State = state;
            Message = message;
            DownloadedAt = downloadedAt;
        }

        /// <summary>
        /// URL to the download status for a pre-receive environment.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The state of the most recent download.
        /// </summary>
        public StringEnum<PreReceiveEnvironmentDownloadState> State { get; private set; }

        /// <summary>
        /// On failure, this will have any error messages produced.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The time when the most recent download started.
        /// </summary>
        public DateTimeOffset? DownloadedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "State: {0} Message: {1}", State, Message); }
        }
    }
}
