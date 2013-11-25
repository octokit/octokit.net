using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
namespace Octokit
{
    public class NewGist
    {
        /// <summary>
        /// The description of the gist.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the gist is public
        /// </summary>
        public bool Public { get; set; }
        
        /// <summary>
        /// Files that make up this gist using the key as Filename
        /// and value as Content
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IDictionary<string, string> Files { get; set; }
    }
}