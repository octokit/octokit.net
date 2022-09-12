using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryContentLicense : RepositoryContentInfo
    {
        public RepositoryContentLicense(LicenseMetadata license, string name, string path, string sha, int size, ContentType type, string downloadUrl, string url, string gitUrl, string htmlUrl)
            : base(name, path, sha, size, type, downloadUrl, url, gitUrl, htmlUrl)
        {
            Ensure.ArgumentNotNull(license, nameof(license));

            License = license;
        }

        public RepositoryContentLicense()
        {
        }

        /// <summary>
        /// License information
        /// </summary>
        public LicenseMetadata License { get; private set; }

        internal new string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "License: {0} {1}", this.License?.Key, base.DebuggerDisplay);
            }
        }
    }
}
