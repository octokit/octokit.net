using System;

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
        public TreeItem[] Tree { get; set; }
    }
}