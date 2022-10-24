﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TreeItem
    {
        public TreeItem() { }

        public TreeItem(string path, string mode, TreeType type, int size, string sha, string url)
        {
            Path = path;
            Mode = mode;
            Type = type;
            Size = size;
            Sha = sha;
            Url = url;
        }

        /// <summary>
        /// The path for this Tree Item.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The mode of this Tree Item.
        /// </summary>
        public string Mode { get; private set; }

        /// <summary>
        /// The type of this Tree Item.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public StringEnum<TreeType> Type { get; private set; }

        /// <summary>
        /// The size of this Tree Item.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The SHA of this Tree Item.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// The URL of this Tree Item.
        /// </summary>
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Sha: {0}, Path: {1}, Type: {2}, Size: {3}", Sha, Path, Type, Size); }
        }
    }

    public enum TreeType
    {
        [Parameter(Value = "blob")]
        Blob,

        [Parameter(Value = "tree")]
        Tree,

        [Parameter(Value = "commit")]
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
