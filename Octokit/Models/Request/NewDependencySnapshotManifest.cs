using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshotManifest
    {
        /// <summary>
        /// Creates a new Dependency Manifest Item.
        /// </summary>
        /// <param name="name">Required. The name of the manifest.</param>
        public NewDependencySnapshotManifest(string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Required. The name of the manifest.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional. The manifest file.
        /// </summary>
        public NewDependencySnapshotManifestFile File { get; set; }

        /// <summary>
        /// Optional. User-defined metadata to store domain-specific information limited to 8 keys with scalar values.
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Optional. A collection of resolved package dependencies.
        /// </summary>
        public IDictionary<string, NewDependencySnapshotResolvedDependency> Resolved { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}
