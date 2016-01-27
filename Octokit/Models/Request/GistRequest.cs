using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to request Gists since a certain date.
    /// </summary>
    /// <remarks>
    /// API docs: https://developer.github.com/v3/gists/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GistRequest"/> class.
        /// </summary>
        /// <param name="since">The date for which only gists updated at or after this time are returned.</param>
        public GistRequest(DateTimeOffset since)
        {
            Since = since;
        }

        /// <summary>
        /// Gets or sets the date for which only gists updated at or after this time are returned.
        /// </summary>
        /// <remarks>
        /// This is sent as a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ 
        /// </remarks>
        /// <value>
        /// The since.
        /// </value>
        public DateTimeOffset Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Since: {0}", Since);
            }
        }
    }
}
