using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update an SSH key
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SshKeyUpdate
    {
        /// <summary>
        /// The SSH Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The title of the SSH key
        /// </summary>
        public string Title { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Key: {0} Title: {1}", Key, Title);
            }
        }
    }
}