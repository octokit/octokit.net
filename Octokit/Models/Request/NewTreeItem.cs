namespace Octokit
{
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public TreeType Type { get; set; }

        /// <summary>
        /// The SHA for this Tree item.
        /// </summary>
        public string Sha { get; set; }
    }
}