using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update a gist and its contents.
    /// </summary>
    /// <remarks>
    /// Note: All files from the previous version of the gist are carried over by default if not included in the
    ///  object. Deletes can be performed by including the filename with a null object.
    /// API docs: https://developer.github.com/v3/gists/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistUpdate
    {
        public GistUpdate()
        {
            Files = new Dictionary<string, GistFileUpdate>();
        }

        public string Description { get; set; }

        /// <summary>
        /// Gets a dictionary of gist files to update.
        /// </summary>
        /// <remarks>
        /// Note: All files from the previous version of the gist are carried over by default if not included in the
        /// hash. Deletes can be performed by including the filename with a `null` hash.
        /// </remarks>
        public IDictionary<string, GistFileUpdate> Files { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Description: {0}", Description); }
        }
    }
}
