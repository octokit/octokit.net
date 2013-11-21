using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
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
    }
}