using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SshKey
    {
        /// <summary>
        /// The system-wide unique Id for this user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The SSH Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The title of the SSH key
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The api URL for this organization.
        /// </summary>
        public string Url { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Title: {0} ", Title);
            }
        }
    }
}