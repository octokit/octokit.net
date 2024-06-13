using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshotManifestFile
    {
        /// <summary>
        /// Optional. The path of the manifest file relative to the root of the Git repository.
        /// </summary>
        public string SourceLocation { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Source Location: {0}", SourceLocation);
            }
        }
    }
}
