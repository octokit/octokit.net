using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a new Tree.
    /// </summary>
    /// <remarks>
    /// The tree creation API will take nested entries as well. If both a tree and a nested path modifying that tree 
    /// are specified, it will overwrite the contents of that tree with the new path contents and write a new tree out.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTree
    {
        public NewTree()
        {
            Tree = new Collection<NewTreeItem>();
        }

        /// <summary>
        /// The SHA1 of the tree you want to update with new data.
        /// </summary>
        public string BaseTree { get; set; }

        /// <summary>
        /// The list of Tree Items for this new Tree item.
        /// </summary>
        public ICollection<NewTreeItem> Tree { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "BaseTree: {0}", BaseTree);
            }
        }
    }
}