using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshotDetector
    {
        /// <summary>
        /// Creates a new Dependency Snapshot Detector.
        /// </summary>
        /// <param name="name">Required. The name of the detector used.</param>
        /// <param name="version">Required. The version of the detector used.</param>
        /// <param name="url">Required. The url of the detector used.</param>
        public NewDependencySnapshotDetector(string name, string version, string url)
        {
            Name = name;
            Version = version;
            Url = url;
        }

        /// <summary>
        /// Required. The name of the detector used.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Required. The version of the detector used.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Required. The url of the detector used.
        /// </summary>
        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Version: {1}", Name, Version);
            }
        }
    }
}
