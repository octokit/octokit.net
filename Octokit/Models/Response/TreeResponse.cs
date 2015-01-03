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
        public string Sha { get; protected set; }

        /// <summary>
        /// The URL for this Tree response.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The list of Tree Items for this Tree response.
        /// </summary>
        public IReadOnlyCollection<TreeItem> Tree { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}