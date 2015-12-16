using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LicenseMetadata
    {
        public LicenseMetadata(string key, string name, Uri url)
        {
            Ensure.ArgumentNotNullOrEmptyString(key, "key");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(url, "url");

            Key = key;
            Name = name;
            Url = url;
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
        /// URL to retrieve details about a license.
        /// </summary>
        public Uri Url { get; protected set; }

        internal virtual string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Key: {0} Name: {1}", Key, Name);
            }
        }
    }
}
