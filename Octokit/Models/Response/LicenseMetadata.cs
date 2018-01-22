using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LicenseMetadata
    {
        public LicenseMetadata(string key, string name, string spdxId, string url, bool featured)
        {
            Ensure.ArgumentNotNullOrEmptyString(key, "key");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(spdxId, "spdxId");
            Ensure.ArgumentNotNull(url, "url");

            Key = key;
            Name = name;
            SpdxId = spdxId;
            Url = url;
            Featured = featured;
        }

        public LicenseMetadata()
        {
        }

        /// <summary>
        /// The 
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// Friendly name of the license.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// SPDX license identifier.
        /// </summary>
        public string SpdxId { get; protected set; }

        /// <summary>
        /// URL to retrieve details about a license.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Whether the license is one of the licenses featured on https://choosealicense.com
        /// </summary>
        public bool Featured { get; protected set; }

        internal virtual string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Key: {0} Name: {1}", Key, Name);
            }
        }
    }
}
