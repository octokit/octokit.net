using Octokit.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshotResolvedDependency
    {
        /// <summary>
        /// Optional. Package-url (PURL) of the dependency.
        /// </summary>
        public string PackageUrl { get; set; }

        /// <summary>
        /// Optional. User-defined metadata to store domain-specific information limited to 8 keys with scalar values..
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Optional. A notation of whether a dependency is requested directly by this manifest or is a dependency of another dependency.
        /// </summary>
        public ResolvedPackageKeyRelationship Relationship { get; set; }

        /// <summary>
        /// Optional. A notation of whether the dependency is required for the primary build artifact (runtime) or is only used for development.
        /// </summary>
        public ResolvedPackageKeyScope Scope { get; set; }

        /// <summary>
        /// Optional. Array of package-url (PURLs) of direct child dependencies.
        /// </summary>
        public Collection<string> Dependencies { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Package Url: {0}", PackageUrl);
            }
        }
    }

    public enum ResolvedPackageKeyRelationship
    {
        /// <summary>
        /// The dependency is requested directly by the manifest.
        /// </summary>
        [Parameter(Value = "direct")]
        Direct,

        /// <summary>
        /// The dependency is a dependency of another dependency.
        /// </summary>
        [Parameter(Value = "indirect")]
        Indirect,
    }

    public enum ResolvedPackageKeyScope
    {
        /// <summary>
        /// The dependency is required for the primary build artifact.
        /// </summary>
        [Parameter(Value = "runtime")]
        Runtime,

        /// <summary>
        /// The dependency is only used for development.
        /// </summary>
        [Parameter(Value = "development")]
        Development,
    }
}
