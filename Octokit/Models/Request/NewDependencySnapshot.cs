using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new dependency snapshot to create via the <see cref="IDependencySubmissionClient.Create(string,string,NewDependencySnapshot)" /> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewDependencySnapshot
    {
        /// <summary>
        /// Creates a new Dependency Snapshot.
        /// </summary>
        /// <param name="version">Required. The version of the repository snapshot submission.</param>
        /// <param name="sha">Required. The commit SHA associated with this dependency snapshot. Maximum length: 40 characters.</param>
        /// <param name="ref">Required. The repository branch that triggered this snapshot.</param>
        /// <param name="scanned">Required. The time at which the snapshot was scanned.</param>
        /// <param name="job">Required. The job associated with this dependency snapshot.</param>
        /// <param name="detector">Required. A description of the detector used.</param>
        public NewDependencySnapshot(long version, string sha, string @ref, string scanned, NewDependencySnapshotJob job, NewDependencySnapshotDetector detector)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNullOrEmptyString(@ref, nameof(@ref));
            Ensure.ArgumentNotNullOrEmptyString(scanned, nameof(scanned));
            Ensure.ArgumentNotNull(job, nameof(job));
            Ensure.ArgumentNotNull(detector, nameof(detector));

            Version = version;
            Sha = sha;
            Ref = @ref;
            Scanned = scanned;
            Job = job;
            Detector = detector;
        }

        /// <summary>
        /// Required. The version of the repository snapshot submission.
        /// </summary>
        public long Version { get; private set; }

        /// <summary>
        /// Required. The job associated with this dependency snapshot.
        /// </summary>
        public NewDependencySnapshotJob Job { get; private set; }

        /// <summary>
        /// Required. The commit SHA associated with this dependency snapshot. Maximum length: 40 characters.
        /// </summary>
        public string Sha { get; private set; }

        /// <summary>
        /// Required. The repository branch that triggered this snapshot.
        /// </summary>
        public string Ref { get; private set; }

        /// <summary>
        /// Required. A description of the detector used.
        /// </summary>
        public NewDependencySnapshotDetector Detector { get; private set; }

        /// <summary>
        /// Optional. User-defined metadata to store domain-specific information limited to 8 keys with scalar values.
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Optional. A collection of package manifests.
        /// </summary>
        public IDictionary<string, NewDependencySnapshotManifest> Manifests { get; set; }

        /// <summary>
        /// Required. The time at which the snapshot was scanned.
        /// </summary>
        public string Scanned { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}, Version: {1}", Sha, Version);
            }
        }
    }
}
