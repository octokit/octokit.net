using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A tree item that would be included as part of a <see cref="NewTree"/> when creating a tree.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTreeItem
    {
        /// <summary>
        /// The file referenced in the tree.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// String of the file mode - one of 100644 for file (blob), 
        /// 100755 for executable (blob), 040000 for subdirectory (tree), 
        /// 160000 for submodule (commit) or 
        /// 120000 for a blob that specifies the path of a symlink
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// The type of tree item this is.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public TreeType Type { get; set; }

        /// <summary>
        /// The SHA for this Tree item.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Gets or sets the The content you want this file to have. GitHub will write this blob out and use that SHA 
        /// for this entry. Use either this, or tree.sha.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "SHA: {0}, Path: {1}, Type: {2}", Sha, Path, Type); }
        }
    }
}