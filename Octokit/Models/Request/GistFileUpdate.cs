using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used as part of a <see cref="GistUpdate" /> to update the name or contents of an existing gist file
    /// </summary>
    /// <remarks>
    /// API docs: https://developer.github.com/v3/gists/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistFileUpdate
    {
        /// <summary>
        /// Gets or sets the new name of the file.
        /// </summary>
        /// <value>
        /// The new name of the file.
        /// </value>
        public string NewFileName { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "NewFileName: {0}", NewFileName); }
        }
    }
}