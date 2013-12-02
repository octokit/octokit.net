using System;
using System.Diagnostics.CodeAnalysis;

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
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

    /// <summary>
    /// The file mode to associate with a tree item
    /// </summary>
    public static class FileMode
    {
        /// <summary>
        /// Mark the tree item as a file (applicable to blobs only)
        /// </summary>
        public const string File = "100644";
        /// <summary>
        /// Mark the tree item as an executable (applicable to blobs only)
        /// </summary>
        public const string Executable = "100755";
        /// <summary>
        /// Mark the tree item as a subdirectory (applicable to trees only)
        /// </summary>
        public const string Subdirectory = "040000";
        /// <summary>
        /// Mark the tree item as a submodule (applicable to commits only)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Submodule")]
        public const string Submodule = "160000";
        /// <summary>
        /// Mark the tree item as a symlink (applicable to blobs only)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Symlink")]
        public const string Symlink = "120000";
    }
}