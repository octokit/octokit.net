using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
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
        /// Note: All files from the previous version of the gist are carried over by default if not included in the hash. 
        /// Deletes can be performed by including the filename with a `null` hash.
        /// </remarks>
        public IDictionary<string, GistFileUpdate> Files { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Description: {0}", Description);
            }
        }
    }

    public class GistFileUpdate
    {
        public string NewFileName { get; set; }
        public string Content { get; set; }
    }
}
