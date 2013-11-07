using System;

namespace Octokit
{
    public class TreeItem
    {
        /// <summary>
        /// The path for this Tree Item.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The mode of this Tree Item.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// The type of this Tree Item.
        /// </summary>
        public TreeType Type { get; set; }

        /// <summary>
        /// The size of this Tree Item.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The SHA of this Tree Item.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// The URL of this Tree Item.
        /// </summary>
        public Uri Url { get; set; }
    }

    public enum TreeType
    {
        Blob,
        Tree,
        Commit
    }
}