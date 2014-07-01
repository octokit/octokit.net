using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TreeResponse
    {
        /// <summary>
        /// The SHA for this Tree response.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The URL for this Tree response.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The list of Tree Items for this Tree response.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<TreeItem> Tree { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}