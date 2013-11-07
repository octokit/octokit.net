namespace Octokit
{
    public class NewTree
    {
        /// <summary>
        /// The SHA1 of the tree you want to update with new data.
        /// </summary>
        public string Base_tree { get; set; }

        /// <summary>
        /// The list of Tree Items for this new Tree item.
        /// </summary>
        public NewTreeItem[] Tree { get; set; }
    }
}