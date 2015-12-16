using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TreeResponse
    {
        public TreeResponse() { }

        public TreeResponse(string sha, Uri url, IReadOnlyList<TreeItem> tree, bool truncated)
        {
            Sha = sha;
            Url = url;
            Tree = tree;
            Truncated = truncated;
        }

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
        public IReadOnlyList<TreeItem> Tree { get; protected set; }

        /// <summary>
        /// Whether the response was truncated due to GitHub API limits.
        /// </summary>
        public bool Truncated { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}