using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TreeResponse
    {
        public TreeResponse() { }

        public TreeResponse(string sha, string url, IReadOnlyList<TreeItem> tree, bool truncated)
        {
            Sha = sha;
            Url = url;
            Tree = tree;
            Truncated = truncated;
        }

        /// <summary>
        /// The SHA for this Tree response.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// The URL for this Tree response.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The list of Tree Items for this Tree response.
        /// </summary>
        public IReadOnlyList<TreeItem> Tree { get; private set; }

        /// <summary>
        /// Whether the response was truncated due to GitHub API limits.
        /// </summary>
        public bool Truncated { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}
