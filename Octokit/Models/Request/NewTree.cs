using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
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
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NewTreeItem> Tree { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "BaseTree: {0}", BaseTree);
            }
        }
    }
}