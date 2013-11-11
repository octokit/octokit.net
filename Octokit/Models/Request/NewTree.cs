using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class NewTree
    {
        /// <summary>
        /// The SHA1 of the tree you want to update with new data.
        /// </summary>
        public string BaseTree { get; set; }

        /// <summary>
        /// The list of Tree Items for this new Tree item.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NewTreeItem> Tree { get; set; }
    }
}