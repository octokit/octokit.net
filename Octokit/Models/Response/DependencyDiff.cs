using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependencyDiff
    {
        public DependencyDiff() { }

        public DependencyDiff(ChangeType changeType, string manifest, string ecosystem, string name, string version, string packageUrl, string license, string sourceRepositoryUrl, IReadOnlyList<DependencyVulnerability> vulnerabilities, Scope scope)
        {
            ChangeType = changeType;
            Manifest = manifest;
            Ecosystem = ecosystem;
            Name = name;
            Version = version;
            PackageUrl = packageUrl;
            License = license;
            SourceRepositoryUrl = sourceRepositoryUrl;
            Vulnerabilities = vulnerabilities;
            Scope = scope;
        }

        /// <summary>
        /// The type of the change.
        /// </summary>
        public StringEnum<ChangeType> ChangeType { get; private set; }

        /// <summary>
        /// The package manifest of the dependency.
        /// </summary>
        public string Manifest { get; private set; }

        /// <summary>
        /// The package ecosystem of the dependency.
        /// </summary>
        public string Ecosystem { get; private set; }

        /// <summary>
        /// The package name of the dependency.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The package version of the dependency.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The package URL of the dependency.
        /// </summary>
        public string PackageUrl { get; private set; }

        /// <summary>
        /// The license of the dependency.
        /// </summary>
        public string License { get; private set; }

        /// <summary>
        /// The URL of the source repository of the dependency.
        /// </summary>
        public string SourceRepositoryUrl { get; private set; }

        /// <summary>
        /// A list of vulnerabilities for the dependency.
        /// </summary>
        public IReadOnlyList<DependencyVulnerability> Vulnerabilities { get; private set; }

        /// <summary>
        /// The scope of the dependency.
        /// </summary>
        public StringEnum<Scope> Scope { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0}, Version: {1}", Name, Version);
            }
        }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependencyVulnerability
    {
        public DependencyVulnerability() { }

        public DependencyVulnerability(string severity, string advisoryGhsaId, string advisorySummary, string advisoryUrl)
        {
            Severity = severity;
            AdvisoryGhsaId = advisoryGhsaId;
            AdvisorySummary = advisorySummary;
            AdvisoryUrl = advisoryUrl;
        }

        /// <summary>
        /// The severity of the vulnerability.
        /// </summary>
        public string Severity { get; private set; }

        /// <summary>
        /// The GHSA Id of the advisory.
        /// </summary>
        public string AdvisoryGhsaId { get; private set; }

        /// <summary>
        /// "A summary of the advisory."
        /// </summary>
        public string AdvisorySummary { get; private set; }

        /// <summary>
        /// The URL of the advisory.
        /// </summary>
        public string AdvisoryUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Advisory URL: {0}", AdvisoryUrl);
            }
        }
    }

}
